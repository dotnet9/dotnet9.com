namespace Dotnet9.Service.Domain.Aggregates.Blogs;

public class BlogAlbum : Entity<Guid>
{
    private BlogAlbum()
    {
    }

    internal BlogAlbum(Guid blogId, Guid albumId)
    {
        BlogId = blogId;
        AlbumId = albumId;
    }

    public Guid BlogId { get; set; }
    public Guid AlbumId { get; set; }
}