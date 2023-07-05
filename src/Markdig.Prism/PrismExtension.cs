using System;
using Markdig.Renderers;
using Markdig.Renderers.Html;

namespace Markdig.Prism
{
  public class PrismExtension : IMarkdownExtension
  {

    private readonly PrismOptions _options;

    public PrismExtension(PrismOptions options)
    {
      _options = options;
    }

    public void Setup(MarkdownPipelineBuilder pipeline)
    {
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
      if (renderer == null)
      {
        throw new ArgumentNullException(nameof(renderer));
      }

      if (renderer is TextRendererBase<HtmlRenderer> htmlRenderer)
      {
        var codeBlockRenderer = htmlRenderer.ObjectRenderers.FindExact<CodeBlockRenderer>();
        if (codeBlockRenderer != null)
        {
          htmlRenderer.ObjectRenderers.Remove(codeBlockRenderer);
        }

        htmlRenderer.ObjectRenderers.AddIfNotAlready(new PrismCodeBlockRenderer(codeBlockRenderer, _options));
      }
    }
  }
}
