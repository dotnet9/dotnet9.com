namespace Dotnet9.Domain.Blogs;

public class BlogPostTag : Entity
{
    private BlogPostTag()
    {
    }

    public BlogPostTag(int blogPostId, int tagId)
    {
        BlogPostId = blogPostId;
        TagId = tagId;
    }

    public int BlogPostId { get; set; }
    public int TagId { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { BlogPostId, TagId };
    }
}