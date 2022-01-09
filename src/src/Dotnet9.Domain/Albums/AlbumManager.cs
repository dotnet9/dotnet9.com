using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Dotnet9.Albums;

public class AlbumManager : DomainService
{
    private readonly IAlbumRepository _albumRepository;

    public AlbumManager(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public async Task<Album> CreateAsync([NotNull] string name, string coverImageUrl, string description)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));

        var existingAlbum = await _albumRepository.FindByNameAsync(name);
        if (existingAlbum != null)
        {
            throw new AlbumAlreadyExistsException(name);
        }

        return new Album(GuidGenerator.Create(), name, coverImageUrl, description);
    }

    public async Task ChangeNameAsync([NotNull] Album album, [NotNull] string newName)
    {
        Check.NotNull(album, nameof(album));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingAlbum = await _albumRepository.FindByNameAsync(newName);
        if (existingAlbum != null && existingAlbum.Id != album.Id)
        {
            throw new AlbumAlreadyExistsException(newName);
        }

        album.ChangeName(newName);
    }
}