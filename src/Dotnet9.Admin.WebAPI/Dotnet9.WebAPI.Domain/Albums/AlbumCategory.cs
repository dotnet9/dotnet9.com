namespace Dotnet9.WebAPI.Domain.Albums;

public record AlbumCategory : BaseEntity
{
    private AlbumCategory()
    {
    }

    internal AlbumCategory(Guid albumId, Guid categoryId)
    {
        AlbumId = albumId;
        CategoryId = categoryId;
    }

    public Guid AlbumId { get; set; }
    public Guid CategoryId { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { CategoryId, AlbumId };
    }
}