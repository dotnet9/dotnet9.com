namespace Dotnet9.Service.Domain.Aggregates.Blogs;

public class BlogViewCount : Entity<Guid>
{
    public string Slug { get; protected set; } = null!;

    public string Ip { get; protected set; } = null!;

    public DateTime CreationTime { get; protected set; }

    public BlogViewCount(string slug, string ip, DateTime creationTime)
    {
        Slug = slug;
        Ip = ip;
        CreationTime = creationTime;
    }
}