using Dotnet9.Core;

namespace Dotnet9.Domain.Albums;

public class AlbumManager
{
    private readonly IAlbumRepository _albumRepository;

    public AlbumManager(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Album> CreateAsync(int? id, string name, string slug, string cover, string? description)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.NotNullOrWhiteSpace(cover, nameof(cover));

        var existAlbum = await _albumRepository.FindByNameAsync(name);
        if (existAlbum != null) throw new Exception($"存在同名的专辑: {name}");

        existAlbum = await _albumRepository.FindBySlugAsync(slug);
        if (existAlbum != null) throw new Exception($"存在相同别名的专辑: {slug}");

        if (id != null) return new Album(id.Value, name, slug, cover, description);

        var maxIdOfAlbum = await _albumRepository.GetMaxIdAsync();
        id = maxIdOfAlbum + 1;

        return new Album(id.Value, name, slug, cover, description);
    }

    public async Task ChangeNameAsync(Album album, string newName)
    {
        Check.NotNull(album, nameof(album));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existAlbum = await _albumRepository.FindByNameAsync(newName);
        if (existAlbum != null && existAlbum.Id != album.Id) throw new Exception("存在同名的专辑");

        album.ChangeName(newName);
    }

    public async Task ChangeSlugAsync(Album album, string newSlug)
    {
        Check.NotNull(album, nameof(album));
        Check.NotNullOrWhiteSpace(newSlug, nameof(newSlug));

        var existAlbum = await _albumRepository.FindBySlugAsync(newSlug);
        if (existAlbum != null && existAlbum.Id != album.Id) throw new Exception("存在相同别名的专辑");

        album.ChangeSlug(newSlug);
    }
}