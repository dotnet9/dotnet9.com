using Dotnet9.Application.Auth;
using Dotnet9.Application.Client.Dtos;

namespace Dotnet9.Application.Client;

/// <summary>
/// 博客说说
/// </summary>
[ApiDescriptionSettings("博客前端接口")]
[AllowAnonymous]
public class TalksController : IDynamicApiController
{
    private readonly ISqlSugarRepository<Talks> _talksRepository;
    private readonly AuthManager _authManager;

    public TalksController(ISqlSugarRepository<Talks> talksRepository,
        AuthManager authManager)
    {
        _talksRepository = talksRepository;
        _authManager = authManager;
    }

    /// <summary>
    /// 说说列表
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageResult<TalksOutput>> Get([FromQuery] Pagination dto)
    {
        long userId = _authManager.UserId;
        return await _talksRepository.AsQueryable().Where(x => x.Status == AvailabilityStatus.Enable)
              .OrderByDescending(x => x.IsTop)
              .OrderByDescending(x => x.Id)
              .Select(x => new TalksOutput
              {
                  Id = x.Id,
                  IsTop = x.IsTop,
                  Content = x.Content,
                  Images = x.Images,
                  Upvote = SqlFunc.Subqueryable<Praise>().Where(p => p.ObjectId == x.Id).Count(),
                  Comments = SqlFunc.Subqueryable<Comments>().Where(c => c.ModuleId == x.Id && c.RootId == null).Count(),
                  IsPraise = SqlFunc.Subqueryable<Praise>().Where(p => p.ObjectId == x.Id && p.AccountId == userId).Any(),
                  CreatedTime = x.CreatedTime
              }).ToPagedListAsync(dto);
    }

    /// <summary>
    /// 说说详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<TalkDetailOutput> TalkDetail([FromQuery] long id)
    {
        long userId = _authManager.UserId;
        return await _talksRepository.AsQueryable()
            .Where(x => x.Id == id)
            .Select(x => new TalkDetailOutput
            {
                Id = x.Id,
                Content = x.Content,
                Images = x.Images,
                IsTop = x.IsTop,
                IsAllowComments = x.IsAllowComments,
                IsPraise = SqlFunc.Subqueryable<Praise>().Where(p => p.ObjectId == x.Id && p.AccountId == userId).Any(),
                Upvote = SqlFunc.Subqueryable<Praise>().Where(p => p.ObjectId == x.Id).Count(),
                CreatedTime = x.CreatedTime
            }).FirstAsync();
    }
}