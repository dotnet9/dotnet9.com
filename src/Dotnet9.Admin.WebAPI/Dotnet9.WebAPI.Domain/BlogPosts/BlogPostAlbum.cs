namespace Dotnet9.WebAPI.Domain.BlogPosts;

public record BlogPostAlbum : BaseEntity
{
    private BlogPostAlbum()
    {
    }

    internal BlogPostAlbum(Guid blogPostId, Guid albumId)
    {
        BlogPostId = blogPostId;
        AlbumId = albumId;
    }

    public Guid BlogPostId { get; set; }
    public Guid AlbumId { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { BlogPostId, AlbumId };
    }
}