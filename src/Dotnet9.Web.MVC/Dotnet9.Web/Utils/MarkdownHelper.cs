using Markdig;
using Markdig.Parsers;
using Markdig.Renderers;

namespace Dotnet9.Web.Utils;

public static class MarkdownHelper
{
    public static string Render(string markdown)
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