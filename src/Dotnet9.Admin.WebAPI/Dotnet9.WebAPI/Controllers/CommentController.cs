namespace Dotnet9.WebAPI.Controllers;

[Route("api/comments/[action]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly IMemoryCacheHelper _cacheHelper;
    private readonly Dotnet9DbContext _dbContext;
    private readonly CommentManager _manager;
    private readonly ICommentRepository _repository;
    private const string GetCommentCacheKey = "CommentController.Get";

    public CommentController(Dotnet9DbContext dbContext, ICommentRepository repository, CommentManager manager,
        IMemoryCacheHelper cacheHelper)
    {
        _dbContext = dbContext;
        _repository = repository;
        _manager = manager;
        _cacheHelper = cacheHelper;
    }

    [HttpGet]
    public async Task<ActionResult<CommentDto[]?>> TopSix()
    {
        // TODO暂时返回一些测试数据 
        return Enumerable.Range(0, 6).Select(index => new CommentDto(Guid.NewGuid(), null, "https://dotnet9.com",
            "testuser", "https://img1.dotnet9.com/site/cat.png",
            "testemail@dotnet9.com", "testcontent", DateTime.Now.ToString())).ToArray();
    }

    [HttpGet]
    [NoWrapper]
    public async Task<ActionResult<CommentDto[]?>> Get([FromQuery] GetCommentListRequest request)
    {
        var avatar = "https://img1.dotnet9.com/site/cat.png";

        async Task<CommentDto[]?> GetDataFromDb()
        {
            var dataFromDb = await _repository.GetListAsync(request);
            return dataFromDb.Comments
                ?.Select(x => new CommentDto(x.Id, x.ParentId, x.Url, x.UserName, avatar, x.Email, x.Content,
                    x.CreationTime.ToString("yyyy-MM-dd HH:mm:ss")))
                .ToArray();
        }

        var cacheKey = $"{GetCommentCacheKey}_{request.Url}_{request.Current}_{request.PageSize}";
        var data = await _cacheHelper.GetOrCreateAsync(cacheKey,
            async e => await GetDataFromDb());
        if (data == null)
        {
            return NotFound();
        }

        return data;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> Add([FromBody] AddCommentRequest request)
    {
        var comment = await _manager.CreateAsync(request.ParentId, request.Url, request.UserName, request.Email,
            request.Content);

        var data = await _dbContext.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
        return new CommentDto(data.Entity.Id, data.Entity.ParentId, data.Entity.Url, data.Entity.UserName, "",
            data.Entity.Email, data.Entity.Content, data.Entity.CreationTime.ToString("yyyy-MM-dd HH:mm:ss"));
    }
}