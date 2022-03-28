using AutoMapper;
using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Domain.Albums;

namespace Dotnet9.Application.Albums;

public class AlbumAppService : IAlbumAppService
{
    private readonly AlbumManager _albumManager;
    private readonly IAlbumRepository _albumRepository;
    private readonly IMapper _mapper;

    public AlbumAppService(IAlbumRepository albumRepository, AlbumManager albumManager, IMapper mapper)
    {
        _albumRepository = albumRepository;
        _albumManager = albumManager;
        _mapper = mapper;
    }

    public async Task<List<AlbumCountDto>> GetListCountAsync()
    {
        var categories = await _albumRepository.GetListCountAsync();

        return _mapper.Map<List<AlbumCount>, List<AlbumCountDto>>(categories);
    }
}