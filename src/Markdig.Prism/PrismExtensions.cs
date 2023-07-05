namespace Markdig.Prism
{
    public static class PrismExtensions
    {
        public static MarkdownPipelineBuilder UsePrism(this MarkdownPipelineBuilder pipeline)
        {
            PrismOptions options = new PrismOptions();
            pipeline.Extensions.Add(new PrismExtension(options));
            return pipeline;
        }
        public static MarkdownPipelineBuilder UsePrism(this MarkdownPipelineBuilder pipeline, PrismOptions options)
        {
            if (options == null)
            {
                options = new PrismOptions();
            }
            pipeline.Extensions.Add(new PrismExtension(options));
            return pipeline;
        }
    }
}
