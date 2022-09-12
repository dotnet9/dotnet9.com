namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActionLogController : ControllerBase
{
    private readonly Dotnet9DbContext _dbContext;
    private readonly ActionLogManager _manager;
    private readonly IActionLogRepository _repository;

    public ActionLogController(Dotnet9DbContext dbContext, IActionLogRepository repository,
        ActionLogManager manager)
    {
        _dbContext = dbContext;
        _repository = repository;
        _manager = manager;
    }

    [HttpGet]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<GetActionLogListResponse> List([FromQuery] GetActionLogListRequest request)
    {
        var result = await _repository.GetListAsync(request.Keywords, request.PageIndex, request.PageSize);
        return new GetActionLogListResponse(result.Logs?.Adapt<ActionLogDto[]>(), result.Count);
    }

    [HttpDelete]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<int> Delete([FromBody] DeleteActionLogRequest request)
    {
        return await _repository.DeleteAsync(request.Ids);
    }

    [HttpPost]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<ActionLogDto> Add([FromBody] AddActionLogRequest request)
    {
        var actionLog = _manager.Create(request.UId, request.Ua, request.Os, request.Browser, request.Ip,
            request.Referer, request.AccessName,
            request.Original, request.Url, request.Controller, request.Action, request.Method, request.Arguments,
            request.Duration);
        var actionLogFromDb = await _dbContext.AddAsync(actionLog);
        await _dbContext.SaveChangesAsync();
        return actionLogFromDb.Entity.Adapt<ActionLogDto>();
    }
}