namespace Markdig.Prism
{
    public static class PrismExtensions
    {
        public static MarkdownPipelineBuilder UsePrism(this MarkdownPipelineBuilder pipeline)
        {
            pipeline.Extensions.Add(new PrismExtension());
            return pipeline;
        }
    }
}
