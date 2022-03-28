namespace Dotnet9.Domain.Blogs;

public class BlogPostAlbum : Entity
{
    private BlogPostAlbum()
    {
    }

    public BlogPostAlbum(int blogPostId, int albumId)
    {
        BlogPostId = blogPostId;
        AlbumId = albumId;
    }

    public int BlogPostId { get; set; }
    public int AlbumId { get; set; }

    public override object[] GetKeys()
    {
        return new object[] {BlogPostId, AlbumId};
    }
}