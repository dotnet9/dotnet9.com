namespace Dotnet9.Service.Domain.Aggregates.Abouts;

public class About : FullAggregateRoot<Guid, int>
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
