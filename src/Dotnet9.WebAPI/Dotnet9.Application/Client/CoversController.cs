using Dotnet9.Application.Client.Dtos;
using Furion.UnifyResult;

namespace Dotnet9.Application.Client;

/// <summary>
/// 模块封面控制器
/// </summary>
[ApiDescriptionSettings("博客前端接口")]
[AllowAnonymous]
public class CoversController : IDynamicApiController
{
    private readonly ISqlSugarRepository<Covers> _coversRepository;

    public CoversController(ISqlSugarRepository<Covers> coversRepository)
    {
        _coversRepository = coversRepository;
    }

    /// <summary>
    /// 模块封面列表
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageResult<CoversOutput>> Get([FromQuery] Pagination dto)
    {
        return await _coversRepository.AsQueryable().Where(x => x.IsVisible && x.Status == AvailabilityStatus.Enable)
              .OrderBy(x => x.Sort)
              .OrderByDescending(x => x.Id)
              .Select(x => new CoversOutput
              {
                  Id = x.Id,
                  Name = x.Name,
                  Cover = x.Cover,
                  Remark = x.Remark,
                  CreatedTime = x.CreatedTime
              }).ToPagedListAsync(dto);
    }

    /// <summary>
    /// 模块下的图片
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageResult<PictureOutput>> Pictures([FromQuery] PicturesQueryInput dto)
    {
        var cover = await _coversRepository.AsQueryable().Where(x => x.Id == dto.CoverId && x.IsVisible && x.Status == AvailabilityStatus.Enable).Select(x => new
        {
            x.Name,
            x.Cover
        }).FirstAsync();
        UnifyContext.Fill(cover);
        return await _coversRepository.AsQueryable().InnerJoin<Pictures>((covers, pictures) => covers.Id == pictures.CoverId)
             .Where(covers => covers.IsVisible && covers.Status == AvailabilityStatus.Enable && covers.Id == dto.CoverId)
             .OrderByDescending((covers, pictures) => pictures.Id)
             .Select((covers, pictures) => new PictureOutput { Id = pictures.Id, Url = pictures.Url }).ToPagedListAsync(dto);
    }
}