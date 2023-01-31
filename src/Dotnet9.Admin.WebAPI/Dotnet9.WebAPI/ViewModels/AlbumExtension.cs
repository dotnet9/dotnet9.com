namespace Dotnet9.WebAPI.ViewModels;

public static class AlbumExtension
{
    public static AlbumDto? ConvertToAlbumDto(this AlbumDto? album, string assetsRemotePath,
        Dictionary<Guid, string>? categoryIdAndNames)
    {
        if (album == null)
        {
            return album;
        }

        album.Cover = album.Cover.StartsWith(assetsRemotePath) ? album.Cover : $"{assetsRemotePath}/{album.Cover}";
        if (categoryIdAndNames != null && categoryIdAndNames.Any() && album.CategoryIds != null &&
            album.CategoryIds.Any())
        {
            album.CategoryNames =
                album.CategoryIds.Select(categoryId => categoryIdAndNames[categoryId]).JoinAsString(",");
        }

        return album;
    }

    public static AlbumDto[]? ConvertToAlbumDtoArray(this AlbumDto[]? albums, string assetsRemotePath,
        Dictionary<Guid, string>? categoryIdAndNames)
    {
        if (albums == null || !albums.Any())
        {
            return null;
        }

        return albums.Select(album => album.ConvertToAlbumDto(assetsRemotePath, categoryIdAndNames)!).ToArray();
    }
}