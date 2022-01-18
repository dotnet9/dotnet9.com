using System;
using Volo.Abp.Domain.Entities;

namespace Dotnet9.Blogs;

public class BlogPostTag : Entity
{
    public Guid BlogPostId { get; protected set; }

    public Guid TagId { get; protected set; }

    private BlogPostTag()
    {
    }

    public BlogPostTag(Guid blogPostId, Guid tagId)
    {
        BlogPostId = blogPostId;
        TagId = tagId;
    }

    public override object[] GetKeys()
    {
        return new object[] { BlogPostId, TagId };
    }
}