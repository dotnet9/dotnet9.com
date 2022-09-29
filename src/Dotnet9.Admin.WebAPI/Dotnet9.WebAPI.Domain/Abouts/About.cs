namespace Dotnet9.WebAPI.Domain.Abouts;

public record About : AggregateRootEntity
{
    private About()
    {
    }

    internal About(Guid id, string content)
    {
        Id = id;
        ChangeContent(content);
    }

    public string Content { get; private set; } = null!;

    public About ChangeContent(string content)
    {
        Content = Check.NotNullOrWhiteSpace(content, nameof(content), AboutConsts.MaxContentLength,
            AboutConsts.MinContentLength);
        return this;
    }
}