using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text;
using System.Web;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdig.Prism
{
    public class PrismCodeBlockRenderer : HtmlObjectRenderer<CodeBlock>
    {
        private readonly CodeBlockRenderer codeBlockRenderer;
        // Adding options to the renderer
        private readonly PrismOptions _options;
        public static readonly Dictionary<string, string> LanguageToFileExtension = new Dictionary<string, string>
        {
            { "javascript", ".js" },
            { "csharp", ".cs" },
            { "python", ".py" },
            { "markup", ".html" },
            { "css", ".css" },
            { "clike", ".js" },
            { "abap", ".abap" },
            { "actionscript", ".as" },
            { "ada", ".ada" },
            { "apacheconf", ".conf" },
            { "apl", ".apl" },
            { "applescript", ".applescript" },
            { "arduino", ".ino" },
            { "arff", ".arff" },
            { "asciidoc", ".adoc" },
            { "asm6502", ".asm" },
            { "aspnet", ".cs" },
            { "autohotkey", ".ahk" },
            { "autoit", ".au3" },
            { "bash", ".sh" },
            { "basic", ".bas" },
            { "batch", ".bat" },
            { "bison", ".bison" },
            { "brainfuck", ".bf" },
            { "bro", ".bro" },
            { "c", ".c" },
            { "cpp", ".cpp" },
            { "coffeescript", ".coffee" },
            { "clojure", ".clj" },
            { "crystal", ".cr" },
            { "csp", ".csp" },
            { "css-extras", ".css" },
            { "d", ".d" },
            { "dart", ".dart" },
            { "diff", ".diff" },
            { "django", ".django" },
            { "docker", ".docker" },
            { "eiffel", ".e" },
            { "elixir", ".ex" },
            { "elm", ".elm" },
            { "erb", ".erb" },
            { "erlang", ".erl" },
            { "fsharp", ".fs" },
            { "flow", ".js" },
            { "fortran", ".f90" },
            { "gedcom", ".ged" },
            { "gherkin", ".feature" },
            { "git", ".git" },
            { "glsl", ".glsl" },
            { "gml", ".gml" },
            { "go", ".go" },
            { "graphql", ".graphql" },
            { "groovy", ".groovy" },
            { "haml", ".haml" },
            { "handlebars", ".hbs" },
            { "haskell", ".hs" },
            { "haxe", ".hx" },
            { "http", ".http" },
            { "hpkp", ".hpkp" },
            { "hsts", ".hsts" },
            { "ichigojam", ".ijam" },
            { "icon", ".icon" },
            { "inform7", ".inform" },
            { "ini", ".ini" },
            { "io", ".io" },
            { "j", ".j" },
            { "java", ".java" },
            { "jolie", ".ol" },
            { "json", ".json" },
            { "julia", ".jl" },
            { "keyman", ".keyman" },
            { "kotlin", ".kt" },
            { "latex", ".tex" },
            { "less", ".less" },
            { "liquid", ".liquid" },
            { "lisp", ".lisp" },
            { "livescript", ".ls" },
            { "lolcode", ".lol" },
            { "lua", ".lua" },
            { "makefile", ".mk" },
            { "markdown", ".md" },
            { "markup-templating", ".html" },
            { "matlab", ".mat" },
            { "mel", ".mel" },
            { "mizar", ".miz" },
            { "monkey", ".monkey" },
            { "n4js", ".n4js" },
            { "nasm", ".asm" },
            { "nginx", ".conf" },
            { "nim", ".nim" },
            { "nix", ".nix" },
            { "nsis", ".nsis" },
            { "objectivec", ".m" },
            { "ocaml", ".ml" },
            { "opencl", ".cl" },
            { "oz", ".oz" },
            { "parigp", ".parigp" },
            { "parser", ".parser" },
            { "pascal", ".pas" },
            { "perl", ".pl" },
            { "php", ".php" },
            { "plsql", ".plsql" },
            { "powershell", ".ps1" },
            { "processing", ".pde" },
            { "prolog", ".pl" },
            { "properties", ".properties" },
            { "protobuf", ".proto" },
            { "pug", ".pug" },
            { "puppet", ".pp" },
            { "pure", ".pure" },
            { "q", ".q" },
            { "qore", ".q" },
            { "r", ".r" },
            { "jsx", ".jsx" },
            { "tsx", ".tsx" },
            { "renpy", ".rpy" },
            { "reason", ".re" },
            { "rest", ".rest" },
            { "rip", ".rip" },
            { "roboconf", ".roboconf" },
            { "ruby", ".rb" },
            { "rust", ".rs" },
            { "sas", ".sas" },
            { "sass", ".sass" },
            { "scss", ".scss" },
            { "scala", ".scala" },
            { "scheme", ".scm" },
            { "smalltalk", ".st" },
            { "smarty", ".tpl" },
            { "sql", ".sql" },
            { "soy", ".soy" },
            { "stylus", ".styl" },
            { "swift", ".swift" },
            { "tap", ".tap" },
            { "tcl", ".tcl" },
            { "textile", ".textile" },
            { "tt2", ".tt2" },
            { "twig", ".twig" },
            { "typescript", ".ts" },
            { "vbnet", ".vb" },
            { "velocity", ".vm" },
            { "verilog", ".v" },
            { "vhdl", ".vhdl" },
            { "vim", ".vim" },
            { "visual-basic", ".vb" },
            { "wasm", ".wasm" },
            { "wiki", ".wiki" },
            { "xeora", ".x" },
            { "xojo", ".xojo" },
            { "xquery", ".xq" },
            { "yaml", ".yaml" }
        };


        public PrismCodeBlockRenderer(CodeBlockRenderer codeBlockRenderer, PrismOptions options)
        {
            this.codeBlockRenderer = codeBlockRenderer ?? new CodeBlockRenderer();
            // If no options are given we use the defaults that are staed in the PrismOptions class
            _options = options ?? new PrismOptions();
        }

        protected override void Write(HtmlRenderer renderer, CodeBlock node)
        {
            var fencedCodeBlock = node as FencedCodeBlock;
            var parser = node.Parser as FencedCodeBlockParser;
            if (fencedCodeBlock == null || parser == null)
            {
                codeBlockRenderer.Write(renderer, node);
                return;
            }

            var languageCode = fencedCodeBlock.Info.Replace(parser.InfoPrefix, string.Empty);
            if (string.IsNullOrWhiteSpace(languageCode) || !PrismSupportedLanguages.IsSupportedLanguage(languageCode))
            {
                codeBlockRenderer.Write(renderer, node);
                return;
            }

            var attributes = new HtmlAttributes();
            attributes.AddClass($"language-{languageCode}");

            if (_options.UseLineNumbers)
            {
                attributes.AddClass("line-numbers");
            }
            renderer.Write("<pre");

            string id = "";

            if (_options.UseDownloadButton)
            {
                // Generate a unique ID for this code block.
                id = Guid.NewGuid().ToString();

                // Add the "data-download-link" and "id" attributes to the <pre> element.
                var preAttributes = new HtmlAttributes();
                preAttributes.AddProperty("data-download-link", "");
                renderer.WriteAttributes(preAttributes);
                attributes.AddProperty("id", id);
            }

            renderer.Write(">")
                    .Write("<code")
                    .WriteAttributes(attributes)
                    .Write(">");

            var code = ExtractSourceCode(node);
            var escapedCode = HttpUtility.HtmlEncode(code);
            renderer.Write(escapedCode)
                   .Write("</code>");

            if (_options.UseDownloadButton)
            {
                string fileExtension;
                if (LanguageToFileExtension.TryGetValue(languageCode, out fileExtension))
                {
                    // The language is supported by PrismJS and has a corresponding file extension.
                    // Add a download button with a "data-extension" attribute.
                    renderer.Write("<button id=\"" + id + "-download\" data-extension=\"" + fileExtension + "\">Download</button>");
                }
                else
                {
                    // The language is not supported by PrismJS or does not have a corresponding file extension.
                    // Add a download button without a "data-extension" attribute, or skip the download button entirely.
                    renderer.Write("<button id=\"" + id + "-download\">Download</button>");
                }
            }

            renderer.Write("</pre>");
        }

        protected string ExtractSourceCode(LeafBlock node)
        {
            var code = new StringBuilder();
            var lines = node.Lines.Lines;
            int totalLines = lines.Length;
            for (int i = 0; i < totalLines; i++)
            {
                var line = lines[i];
                var slice = line.Slice;
                if (slice.Text == null)
                {
                    continue;
                }

                var lineText = slice.Text.Substring(slice.Start, slice.Length);
                if (i > 0)
                {
                    code.AppendLine();
                }

                code.Append(lineText);
            }

            return code.ToString();
        }
    }
}
