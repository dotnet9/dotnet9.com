using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Domain.Albums;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public partial class AlbumController
{
    [HttpGet("api/album/list")]
    public async Task<List<AlbumDto>> List()
    {
        return await _albumAppService.AdminListAsync();
    }

    [HttpDelete("api/album/delete")]
    public async Task Delete(int id)
    {
        await _albumRepository.DeleteAsync(x => x.Id == id);
    }

    [HttpPost("api/album/addOrUpdate")]
    public async Task AddOrUpdate([FromBody] AddOrUpdateAlbumDto request)
    {
        if (request.Id is null or 0)
        {
            var albumForDb = _mapper.Map<AddOrUpdateAlbumDto, Album>(request);
            albumForDb.Id = await _albumRepository.GetMaxIdAsync() + 1;
            albumForDb.CreateDate = DateTimeOffset.Now;
            await _albumRepository.InsertAsync(albumForDb);
        }
        else
        {
            var albumFromDb = await _albumRepository.GetAsync(x => x.Id == request.Id);
            if (albumFromDb == null) return;

            _mapper.Map(request, albumFromDb, typeof(AddOrUpdateAlbumDto), typeof(Album));
            albumFromDb.UpdateDate = DateTimeOffset.Now;
            await _albumRepository.UpdateAsync(albumFromDb);
        }
    }
}