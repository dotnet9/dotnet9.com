using Dotnet9.Application.Auth;
using Dotnet9.Application.Blog.Dtos;

namespace Dotnet9.Application.Blog;

/// <summary>
/// 模块封面图片管理
/// </summary>
public class PicturesService : IDynamicApiController
{
    private readonly ISqlSugarRepository<Pictures> _repository;
    private readonly AuthManager _authManager;

    public PicturesService(ISqlSugarRepository<Pictures> repository,
        AuthManager authManager)
    {
        _repository = repository;
        _authManager = authManager;
    }
    /// <summary>
    /// 模块封面分页
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("模块封面分页")]
    [HttpGet]
    [AllowAnonymous]
    public async Task<PageResult<PicturesPageOutput>> Page([FromQuery] PicturesPageQueryInput dto)
    {
        return await _repository.AsQueryable().InnerJoin<Covers>((pictures, covers) => pictures.CoverId == covers.Id)
            .Where(pictures => pictures.CoverId == dto.Id)
            .WhereIF(_authManager.AuthPlatformType is null or AuthPlatformType.Blog, (pictures, covers) => covers.IsVisible)
            .Select(pictures => new PicturesPageOutput { Id = pictures.Id, Url = pictures.Url })
            .ToPagedListAsync(dto);
    }

    /// <summary>
    /// 上传图片到模块
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("上传图片到模块")]
    [HttpPost("add")]
    public async Task Add(AddPictureInput dto)
    {
        var list = dto.Adapt<Pictures>();
        await _repository.InsertAsync(list);
    }

    /// <summary>
    /// 删除模块图片
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("删除模块图片")]
    [HttpDelete("delete")]
    public async Task Delete(KeyDto dto)
    {
        await _repository.DeleteAsync(x => x.Id == dto.Id);
    }
}