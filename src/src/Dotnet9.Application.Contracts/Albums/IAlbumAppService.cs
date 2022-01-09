using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Dotnet9.Albums;

public interface IAlbumAppService : IApplicationService
{
    Task<AlbumDto> GetAsync(Guid id);

    Task<PagedResultDto<AlbumDto>> GetListAsync(GetAlbumListDto input);

    Task<AlbumDto> CreateAsync(CreateAlbumDto input);

    Task UpdateAsync(Guid id, UpdateAlbumDto input);

    Task DeleteAsync(Guid id);
}