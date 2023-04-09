namespace Dotnet9.Service.Domain.Aggregates.Blogs;

public class BlogCategory : Entity<Guid>
{
    private BlogCategory()
    {
    }

    public BlogCategory(Guid blogId, Guid categoryId)
    {
        BlogId = blogId;
        CategoryId = categoryId;
    }

    public Guid BlogId { get; set; }
    public Guid CategoryId { get; set; }
}