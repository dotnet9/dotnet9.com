using Dotnet9.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.Albums;

public class AlbumAppService : Dotnet9AppService, IAlbumAppService
{
    private readonly IAlbumRepository _albumRepository;
    private readonly AlbumManager _albumManager;

    public AlbumAppService(IAlbumRepository albumRepository, AlbumManager albumManager)
    {
        _albumRepository = albumRepository;
        _albumManager = albumManager;
    }

    public async Task<AlbumDto> GetAsync(Guid id)
    {
        var album = await _albumRepository.GetAsync(id);
        return ObjectMapper.Map<Album, AlbumDto>(album);
    }

    public async Task<PagedResultDto<AlbumDto>> GetListAsync(GetAlbumListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Album.Name);
        }

        var albums = await _albumRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting,
            input.Filter);

        var totalCount = input.Filter == null
            ? await _albumRepository.CountAsync()
            : await _albumRepository.CountAsync(album => album.Name.Contains(input.Filter));

        return new PagedResultDto<AlbumDto>(totalCount, ObjectMapper.Map<List<Album>, List<AlbumDto>>(albums));
    }

    [Authorize(Dotnet9Permissions.Albums.Create)]
    public async Task<AlbumDto> CreateAsync(CreateAlbumDto input)
    {
        var album = await _albumManager.CreateAsync(input.Name, input.CoverImageUrl, input.Description);

        await _albumRepository.InsertAsync(album);

        return ObjectMapper.Map<Album, AlbumDto>(album);
    }

    [Authorize(Dotnet9Permissions.Albums.Edit)]
    public async Task UpdateAsync(Guid id, UpdateAlbumDto input)
    {
        var album = await _albumRepository.GetAsync(id);

        if (album.Name != input.Name)
        {
            await _albumManager.ChangeNameAsync(album, input.Name);
        }

        album.CoverImageUrl = input.CoverImageUrl;
        album.Description = input.Description;

        await _albumRepository.UpdateAsync(album);
    }

    [Authorize(Dotnet9Permissions.Albums.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _albumRepository.DeleteAsync(id);
    }
}