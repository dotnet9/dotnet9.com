using Dotnet9.Application.Auth;
using Dotnet9.Application.Blog.Dtos;

namespace Dotnet9.Application.Blog;

/// <summary>
/// 说说管理
/// </summary>
public class TalksService : BaseService<Talks>
{
    private readonly ISqlSugarRepository<Talks> _repository;
    private readonly AuthManager _authManager;

    public TalksService(ISqlSugarRepository<Talks> repository, AuthManager authManager) : base(repository)
    {
        _repository = repository;
        _authManager = authManager;
    }

    /// <summary>
    /// 说说分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("说说分页查询")]
    [HttpGet]
    public async Task<PageResult<TalksPageOutput>> Page([FromQuery] TalksPageQueryInput dto)
    {
        long userId = _authManager.UserId;
        return await _repository.AsQueryable()
              .WhereIF(!string.IsNullOrWhiteSpace(dto.Keyword), x => x.Content.Contains(dto.Keyword))
              .OrderByDescending(x => x.Id)
              .Select(x => new TalksPageOutput
              {
                  Id = x.Id,
                  Status = x.Status,
                  Content = x.Content,
                  Images = x.Images,
                  IsAllowComments = x.IsAllowComments,
                  IsTop = x.IsTop,
                  IsPraise = SqlFunc.Subqueryable<Praise>().Where(p => p.ObjectId == x.Id && p.AccountId == userId).Any(),
                  CreatedTime = x.CreatedTime
              }).ToPagedListAsync(dto);
    }

    /// <summary>
    /// 添加说说
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("添加说说")]
    [HttpPost("add")]
    public async Task Add(AddTalksInput dto)
    {
        var talks = dto.Adapt<Talks>();
        await _repository.InsertAsync(talks);
    }

    /// <summary>
    /// 更新说说
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("更新说说")]
    [HttpPut("edit")]
    public async Task Update(UpdateTalksInput dto)
    {
        var talks = await _repository.GetByIdAsync(dto.Id);
        dto.Adapt(talks);
        await _repository.UpdateAsync(talks);
    }
}