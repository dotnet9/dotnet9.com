using Dotnet9.Application.Blog.Dtos;

namespace Dotnet9.Application.Blog;

/// <summary>
/// 友情链接
/// </summary>
public class FriendLinkService : BaseService<FriendLink>, IDynamicApiController
{
    private readonly ISqlSugarRepository<FriendLink> _repository;

    public FriendLinkService(ISqlSugarRepository<FriendLink> repository) : base(repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 友情链接分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("友情链接分页查询")]
    [HttpGet]
    public async Task<PageResult<FriendLinkPageOutput>> Page([FromQuery] FriendLinkPageQueryInput dto)
    {
        return await _repository.AsQueryable()
              .WhereIF(!string.IsNullOrWhiteSpace(dto.SiteName), x => x.SiteName.Contains(dto.SiteName))
              .OrderByDescending(x => x.Id)
              .Select(x => new FriendLinkPageOutput
              {
                  Id = x.Id,
                  Status = x.Status,
                  SiteName = x.SiteName,
                  CreatedTime = x.CreatedTime,
                  IsIgnoreCheck = x.IsIgnoreCheck,
                  Link = x.Link,
                  Logo = x.Logo,
                  Url = x.Url,
                  Sort = x.Sort,
                  Remark = x.Remark
              }).ToPagedListAsync(dto);
    }

    /// <summary>
    /// 添加友情链接
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("添加友情链接")]
    [HttpPost("add")]
    public async Task Add(AddFriendLinkInput dto)
    {
        var link = dto.Adapt<FriendLink>();
        await _repository.InsertAsync(link);
    }

    [DisplayName("更新友情链接")]
    [HttpPut("edit")]
    public async Task Update(UpdateFriendLinkInput dto)
    {
        var link = await _repository.GetByIdAsync(dto.Id);
        if (link == null) throw Oops.Oh("无效参数");
        dto.Adapt(link);
        await _repository.UpdateAsync(link);
    }
}