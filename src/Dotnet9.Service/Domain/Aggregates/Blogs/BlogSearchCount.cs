namespace Dotnet9.Service.Domain.Aggregates.Blogs;

public class BlogSearchCount : Entity<Guid>
{
    public string Keywords { get; protected set; } = null!;

    public string Ip { get; protected set; } = null!;

    public DateTime CreationTime { get; protected set; }

    public BlogSearchCount(string keywords, string ip, DateTime creationTime)
    {
        Keywords = keywords;
        Ip = ip;
        CreationTime = creationTime;
    }
}