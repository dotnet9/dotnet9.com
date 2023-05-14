namespace Dotnet9.Service.Domain.Aggregates.Blogs;

public class BlogTag : Entity<Guid>
{
    private BlogTag()
    {
    }

    public BlogTag(Guid blogId, Guid tagId)
    {
        BlogId = blogId;
        TagId = tagId;
    }

    public Guid BlogId { get; set; }
    public Guid TagId { get; set; }
}