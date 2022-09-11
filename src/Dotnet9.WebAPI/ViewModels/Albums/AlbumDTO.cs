namespace Dotnet9.WebAPI.ViewModels.Albums;

// ReSharper disable once InconsistentNaming
public record AlbumDTO(Guid Id, Guid[] CategoryIds, int SequenceNumber, string Name, string Slug, string Cover,
    string? Description = null, bool Visible = false);

public static class AlbumExtention
{
    public static AlbumDTO? ConvertToAlbumDto(this Album? album)
    {
        if (album == null)
        {
            return null;
        }

        var categoryIds = album.Categories!.Select(category => category.CategoryId).ToArray();
        return new AlbumDTO(album.Id, categoryIds, album.SequenceNumber, album.Name, album.Slug, album.Cover,
            album.Description, album.Visible);
    }

    public static AlbumDTO[]? ConvertToAlbumDtoArray(this Album[]? albums)
    {
        if (albums == null || !albums.Any())
        {
            return null;
        }

        return albums.Select(album =>
        {
            var categoryIds = album.Categories!.Select(category => category.CategoryId).ToArray();
            return new AlbumDTO(album.Id, categoryIds, album.SequenceNumber, album.Name, album.Slug, album.Cover,
                album.Description, album.Visible);
        }).ToArray();
    }
}