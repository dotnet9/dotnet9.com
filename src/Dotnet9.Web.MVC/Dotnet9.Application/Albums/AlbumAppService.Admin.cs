using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Domain.Albums;
using Dotnet9.Domain.Repositories;

namespace Dotnet9.Application.Albums;

public partial class AlbumAppService
{
    public async Task<List<AlbumDto>> AdminListAsync()
    {
        var albums = await _albumRepository.SelectAsync(x => x.Id > 0, x => x.Id, SortDirectionKind.Ascending);

        return _mapper.Map<List<Album>, List<AlbumDto>>(albums);
    }
}