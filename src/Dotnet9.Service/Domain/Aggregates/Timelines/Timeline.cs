namespace Dotnet9.Service.Domain.Aggregates.Timelines;

public class Timeline : FullAggregateRoot<Guid, int>
{
    private Timeline()
    {
    }

    internal Timeline(Guid id, DateTime time, string title, string content)
    {
        Id = id;
        ChangeTime(time);
        ChangeTitle(title);
        ChangeContent(content);
    }

    public DateTime Time { get; private set; }
    public string Title { get; private set; } = null!;
    public string Content { get; private set; } = null!;


    public Timeline ChangeTime(DateTime time)
    {
        Time = time;
        return this;
    }

    public Timeline ChangeTitle(string title)
    {
        Title = Check.NotNullOrWhiteSpace(title, nameof(title), TimelineConsts.MaxTitleLength,
            TimelineConsts.MinTitleLength);
        return this;
    }

    public Timeline ChangeContent(string content)
    {
        Content = Check.NotNullOrWhiteSpace(content, nameof(content), TimelineConsts.MaxContentLength,
            TimelineConsts.MinContentLength);
        return this;
    }
}