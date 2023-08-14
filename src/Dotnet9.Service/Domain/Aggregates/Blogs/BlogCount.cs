namespace Dotnet9.Service.Domain.Aggregates.Blogs;

public class BlogCount : Entity<Guid>
{
    public Guid BlogId { get; set; }

    public string Ip { get; protected set; } = null!;

    public BlogCountKind Kind { get; set; }

    public DateTime CreationTime { get; protected set; }

    public BlogCount(Guid blogId, string ip, BlogCountKind kind, DateTime creationTime)
    {
        BlogId = blogId;
        Ip = ip;
        Kind = kind;
        CreationTime = creationTime;
    }
}

public enum BlogCountKind
{
    View,
    Like
}