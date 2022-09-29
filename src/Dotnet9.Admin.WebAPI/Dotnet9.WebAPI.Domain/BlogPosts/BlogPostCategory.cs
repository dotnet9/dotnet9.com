namespace Dotnet9.WebAPI.Domain.BlogPosts;

public record BlogPostCategory : BaseEntity
{
    private BlogPostCategory()
    {
    }

    public BlogPostCategory(Guid blogPostId, Guid categoryId)
    {
        BlogPostId = blogPostId;
        CategoryId = categoryId;
    }

    public Guid BlogPostId { get; set; }
    public Guid CategoryId { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { BlogPostId, CategoryId };
    }
}