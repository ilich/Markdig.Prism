﻿using System;
using System.Linq;
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

        public PrismCodeBlockRenderer(CodeBlockRenderer codeBlockRenderer)
        {
            this.codeBlockRenderer = codeBlockRenderer ?? new CodeBlockRenderer();
        }

        private readonly string[] xmlLikeLanguages = new[]
        {
            "xml", "xml-doc", "html"
        };

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
            
            var code = ExtractSourceCode(node);
            var escapedCode = HttpUtility.HtmlEncode(code);

            if (xmlLikeLanguages.Contains(languageCode))
            {
                // Support markup languages
                // https://prismjs.com/plugins/unescaped-markup/
                renderer
                    .Write("<script type='text/plain'")
                    .WriteAttributes(attributes)
                    .Write(">")
                    .Write(escapedCode)
                    .Write("</script>");
            }
            else
            {
                renderer
                    .Write("<pre>")
                    .Write("<code")
                    .WriteAttributes(attributes)
                    .Write(">")
                    .Write(escapedCode)
                    .Write("</code>")
                    .Write("</pre>");

            }
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
