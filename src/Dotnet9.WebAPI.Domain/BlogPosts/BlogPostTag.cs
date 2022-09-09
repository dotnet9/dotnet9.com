namespace Dotnet9.WebAPI.Domain.BlogPosts;

public record BlogPostTag : BaseEntity
{
    private BlogPostTag()
    {
    }

    public BlogPostTag(Guid blogPostId, Guid tagId)
    {
        BlogPostId = blogPostId;
        TagId = tagId;
    }

    public Guid BlogPostId { get; set; }
    public Guid TagId { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { BlogPostId, TagId };
    }
}