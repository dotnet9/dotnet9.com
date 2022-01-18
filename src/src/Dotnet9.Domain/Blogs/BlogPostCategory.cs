using System;
using Volo.Abp.Domain.Entities;

namespace Dotnet9.Blogs;

public class BlogPostCategory : Entity
{
    public Guid BlogPostId { get; protected set; }

    public Guid CategoryId { get; protected set; }

    private BlogPostCategory()
    {
    }

    public BlogPostCategory(Guid blogPostId, Guid categoryId)
    {
        BlogPostId = blogPostId;
        CategoryId = categoryId;
    }

    public override object[] GetKeys()
    {
        return new object[] { BlogPostId, CategoryId };
    }
}