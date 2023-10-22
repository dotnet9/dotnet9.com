using Dotnet9.Application.User.Dtos;

namespace Dotnet9.Application.User;

/// <summary>
/// 博客授权用户
/// </summary>
public class AuthAccountService : IDynamicApiController
{
    private readonly ISqlSugarRepository<AuthAccount> _repository;

    public AuthAccountService(ISqlSugarRepository<AuthAccount> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 博客授权用户列表
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("博客授权用户列表")]
    [HttpGet]
    public async Task<PageResult<AuthAccountPageOutput>> List([FromQuery] AuthAccountPageQueryInput dto)
    {
        return await _repository.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(dto.Name), x => x.Name.Contains(dto.Name))
            .OrderByDescending(x => x.Id)
             .Select(x => new AuthAccountPageOutput
             {
                 Id = x.Id,
                 Name = x.Name,
                 Gender = x.Gender,
                 Type = x.Type,
                 IsBlogger = x.IsBlogger,
                 Avatar = x.Avatar,
                 CreatedTime = x.CreatedTime
             }).ToPagedListAsync(dto);
    }

    /// <summary>
    /// 设置博主
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("设置博主")]
    [HttpPatch("SetBlogger")]
    public async Task SetBlogger(KeyDto dto)
    {
        await _repository.UpdateAsync(x => new AuthAccount()
        {
            IsBlogger = !x.IsBlogger
        }, x => x.Id == dto.Id);
    }

    /// <summary>
    /// 删除博客用户
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("删除博客用户")]
    [HttpDelete("delete")]
    public async Task Delete(KeyDto dto)
    {
        await _repository.UpdateAsync(x => new AuthAccount()
        {
            DeleteMark = true
        }, x => x.Id == dto.Id);
    }
}