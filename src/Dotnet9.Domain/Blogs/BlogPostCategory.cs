namespace Dotnet9.Domain.Blogs;

public class BlogPostCategory : Entity
{
    private BlogPostCategory()
    {
    }

    public BlogPostCategory(int blogPostId, int categoryId)
    {
        BlogPostId = blogPostId;
        CategoryId = categoryId;
    }

    public int BlogPostId { get; set; }
    public int CategoryId { get; set; }

    public override object[] GetKeys()
    {
        return new object[] {BlogPostId, CategoryId};
    }
}