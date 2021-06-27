using System;
using System.Linq;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Markdig.Prism.Tests
{
    [TestClass]
    public class PrismCodeBlockRendererTests
    {
        private static readonly String TestMarkdownWithLanguage = @"
# Sample Dockerfile

```docker
FROM nginx
ENV AUTHOR=Docker

WORKDIR /usr/share/nginx/html
COPY Hello_docker.html /usr/share/nginx/html

CMD cd /usr/share/nginx/html && sed -e s/Docker/""$AUTHOR""/ Hello_docker.html > index.html ; nginx -g 'daemon off;'
```

Use **docker** command to build the imaage from this Dockerfile.
";

        private static readonly string TestMarkdownWithoutLanguage = "Use ```docker run .``` command to build the image";

        private static readonly string TestMarkdownWithUnsupportedLanguage = "Simple graph ```mermaid graph TD; A --> B``` as an example";

        private static readonly MarkdownPipeline pipeline = new MarkdownPipelineBuilder()
                .UseAdvancedExtensions()
                .UsePrism()
                .Build();

        [TestMethod]
        public void RenderValidPrismCodeBlock()
        {
            var html = Markdown.ToHtml(TestMarkdownWithLanguage, pipeline);
            
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var pre = doc.DocumentNode.SelectSingleNode("//pre");
            Assert.IsNotNull(pre);
            var code = pre.ChildNodes.First();
            Assert.AreEqual("code", code.Name);
            var className = code.Attributes.First(a => a.Name == "class");
            Assert.AreEqual("language-docker", className.Value);
            Assert.IsTrue(code.InnerHtml.IndexOf("FROM nginx") > -1);
        }

        [TestMethod]
        public void UseDefaultCodeBlockRendererIfNoLanguageSpecified()
        {
            var html = Markdown.ToHtml(TestMarkdownWithoutLanguage, pipeline);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var pre = doc.DocumentNode.SelectSingleNode("//pre");
            Assert.IsNull(pre);
            var code = doc.DocumentNode.SelectSingleNode("//code");
            Assert.IsNotNull(code);
            Assert.AreEqual("docker run .", code.InnerHtml);
        }

        [TestMethod]
        public void UseDefaultCodeBlockRendererIfLanguageIsNotSupported()
        {
            var html = Markdown.ToHtml(TestMarkdownWithUnsupportedLanguage, pipeline);

            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            var pre = doc.DocumentNode.SelectSingleNode("//pre");
            Assert.IsNull(pre);
            var code = doc.DocumentNode.SelectSingleNode("//code");
            Assert.IsNotNull(code);
            Assert.AreEqual("mermaid graph TD; A --&gt; B", code.InnerHtml);
        }
    }
}
