namespace Dotnet9.WebAPI.ViewModels;

public static class AlbumExtension
{
    public static AlbumDto? ConvertToAlbumDto(this Album? album, string assetsRemotePath,
        Dictionary<Guid, string>? categoryIdAndNames)
    {
        if (album == null)
        {
            return null;
        }

        List<AlbumCategory> queryList = album.Categories!.Where(category =>
            categoryIdAndNames != null && categoryIdAndNames.ContainsKey(category.CategoryId)).ToList();
        string categoryNames = queryList.Select(category => categoryIdAndNames![category.CategoryId]).JoinAsString(",");
        Guid[] categoryIds = queryList.Select(category => category.CategoryId).ToArray();
        return new AlbumDto(album.Id, categoryNames, categoryIds, album.SequenceNumber, album.Name, album.Slug,
            $"{assetsRemotePath}/{album.Cover}",
            album.Description, album.Visible);
    }

    public static AlbumDto[]? ConvertToAlbumDtoArray(this Album[]? albums, string assetsRemotePath,
        Dictionary<Guid, string>? categoryIdAndNames)
    {
        if (albums == null || !albums.Any())
        {
            return null;
        }

        return albums.Select(album => album.ConvertToAlbumDto(assetsRemotePath, categoryIdAndNames)!).ToArray();
    }
}