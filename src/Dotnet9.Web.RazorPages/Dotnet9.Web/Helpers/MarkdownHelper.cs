namespace Dotnet9.Web.Helpers;

public static class MarkdownHelper
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