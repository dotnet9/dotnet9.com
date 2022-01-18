using System;
using Volo.Abp.Domain.Entities;

namespace Dotnet9.Blogs;

public class BlogPostAlbum : Entity
{
    public Guid BlogPostId { get; protected set; }

    public Guid AlbumId { get; protected set; }

    private BlogPostAlbum()
    {
    }

    public BlogPostAlbum(Guid blogPostId, Guid albumId)
    {
        BlogPostId = blogPostId;
        AlbumId = albumId;
    }

    public override object[] GetKeys()
    {
        return new object[] { BlogPostId, AlbumId };
    }
}