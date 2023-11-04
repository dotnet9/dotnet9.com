using Dotnet9.Application.Auth;
using Dotnet9.Application.Client.Dtos;

namespace Dotnet9.Application.Client;

/// <summary>
/// 评论
/// </summary>
[ApiDescriptionSettings("博客前端接口")]
public class CommentController : IDynamicApiController
{
    private readonly ISqlSugarRepository<Comments> _repository;
    private readonly ISqlSugarRepository<Praise> _praiseRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly AuthManager _authManager;

    public CommentController(ISqlSugarRepository<Comments> repository,
        ISqlSugarRepository<Praise> praiseRepository,
        IHttpContextAccessor httpContextAccessor, AuthManager authManager)
    {
        _repository = repository;
        _praiseRepository = praiseRepository;
        _httpContextAccessor = httpContextAccessor;
        _authManager = authManager;
    }

    /// <summary>
    /// 评论列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<PageResult<CommentOutput>> Get([FromQuery] CommentPageQueryInput dto)
    {
        long userId = _authManager.UserId;
        var result = await _repository.AsQueryable().LeftJoin<AuthAccount>((c, account) => c.AccountId == account.Id)
            .Where(c => c.ModuleId == dto.Id && c.RootId == null) //排除回复的评论
            .OrderByDescending(c => c.Id)
            .Select((c, account) => new CommentOutput
            {
                Id = c.Id,
                Content = c.Content,
                PraiseTotal = SqlFunc.Subqueryable<Praise>().Where(x => x.ObjectId == c.Id).Count(),
                IsPraise = SqlFunc.Subqueryable<Praise>().Where(x => x.ObjectId == c.Id && x.AccountId == userId).Any(),
                ReplyCount = SqlFunc.Subqueryable<Comments>().Where(s => s.RootId == c.Id).Count(),
                IP = c.IP,
                Avatar = account.Avatar!,
                AccountId = account.Id,
                NickName = account.Name!,
                IsBlogger = account.IsBlogger,
                Geolocation = c.Geolocation,
                CreatedTime = c.CreatedTime
            })
            //.Mapper(it =>
            //{
            //    if (it.ReplyCount > 0)
            //    {
            //        it.ReplyList = ReplyList(new CommentPageQueryInput()
            //        {
            //            PageNo = 1,
            //            Id = it.Id
            //        }).GetAwaiter().GetResult();
            //    }
            //})
            .ToPagedListAsync(dto);
        return result;
    }

    /// <summary>
    /// 回复分页
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public async Task<PageResult<ReplyOutput>> ReplyList([FromQuery] CommentPageQueryInput dto)
    {
        long userId = _authManager.UserId;
        return await _repository.AsQueryable().LeftJoin<AuthAccount>((c, a1) => c.AccountId == a1.Id)
            .LeftJoin<AuthAccount>((c, a1, a2) => c.ReplyAccountId == a2.Id)
            .Where(c => c.RootId == dto.Id)
            .OrderBy(c => c.Id)
            .Select((c, a1, a2) => new ReplyOutput
            {
                Id = c.Id,
                Content = c.Content,
                ParentId = c.ParentId,
                AccountId = c.AccountId,
                ReplyAccountId = c.ReplyAccountId,
                IsBlogger = a1.IsBlogger,
                NickName = a1.Name!,
                RelyNickName = a2.Name!,
                RootId = c.RootId,
                Avatar = a1.Avatar!,
                PraiseTotal = SqlFunc.Subqueryable<Praise>().Where(x => x.ObjectId == c.Id).Count(),
                IsPraise = SqlFunc.Subqueryable<Praise>().Where(x => x.ObjectId == c.Id && x.AccountId == userId).Any(),
                IP = c.IP,
                Geolocation = c.Geolocation,
                CreatedTime = c.CreatedTime
            }).ToPagedListAsync(dto);
    }

    /// <summary>
    /// 评论、回复
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task Add(AddCommentInput dto)
    {
        var address = _httpContextAccessor.HttpContext!.GetGeolocation();
        var comments = dto.Adapt<Comments>();
        comments.AccountId = _authManager.UserId;
        comments.IP = _httpContextAccessor.HttpContext!.GetRemoteIp();
        comments.Geolocation = address;
        await _repository.InsertAsync(comments);
    }

    /// <summary>
    /// 点赞/取消点赞
    /// </summary>
    /// <param name="dto">对象ID</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<bool> Praise(KeyDto dto)
    {
        if (await _praiseRepository.IsAnyAsync(x => x.ObjectId == dto.Id))
        {
            return await _praiseRepository.DeleteAsync(x => x.ObjectId == dto.Id)
                ? false
                : throw Oops.Oh("糟糕，取消失败了...");
        }

        var praise = new Praise()
        {
            AccountId = _authManager.UserId,
            ObjectId = dto.Id,
        };
        return await _praiseRepository.InsertAsync(praise) ? true : throw Oops.Oh("糟糕，点赞失败了...");
    }
}