using Markdig;
using Markdig.Parsers;
using Markdig.Renderers;

namespace Dotnet9.Commons;

public static class MarkdownUtil
{
    public static string Convert2Html(this string markdown)
    {
        var pipeline = new MarkdownPipelineBuilder()
            .UseAdvancedExtensions()
            .UsePipeTables()
            .Build();
        var document = MarkdownParser.Parse(markdown, pipeline);

        var writer = new StringWriter();
        var renderer = new HtmlRenderer(writer);
        pipeline.Setup(renderer);
        renderer.Render(document);
        writer.Flush();
        return writer.ToString();
    }
}