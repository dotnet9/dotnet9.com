using Masa.BuildingBlocks.Ddd.Domain.Entities;

namespace Dotnet9.Service.Domain.Aggregates.Albums;

public class AlbumCategory : Entity<Guid>
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
}