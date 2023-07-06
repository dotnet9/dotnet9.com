namespace Dotnet9.Service.Domain.Aggregates.Blogs;

public class BlogSearchCount : Entity<Guid>
{
    public string Keywords { get; protected set; } = null!;
    public bool IsEmpty { get; set; }

    public string Ip { get; protected set; } = null!;

    public DateTime CreationTime { get; protected set; }

    public BlogSearchCount(string keywords, bool isEmpty, string ip, DateTime creationTime)
    {
        Keywords = keywords;
        IsEmpty = isEmpty;
        Ip = ip;
        CreationTime = creationTime;
    }
}