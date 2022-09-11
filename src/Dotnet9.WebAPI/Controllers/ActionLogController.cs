namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActionLogController : ControllerBase
{
    private readonly Dotnet9DbContext _dbContext;
    private readonly ActionLogDomainService _domainService;
    private readonly IActionLogRepository _repository;

    public ActionLogController(Dotnet9DbContext dbContext, IActionLogRepository repository,
        ActionLogDomainService domainService)
    {
        _dbContext = dbContext;
        _repository = repository;
        _domainService = domainService;
    }

    [HttpGet]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<QueryActionLogResponse> List([FromQuery] QueryActionLogRequest request)
    {
        return await _repository.List(request.Keywords, request.PageIndex, request.PageSize);
    }

    [HttpDelete]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<int> Delete([FromBody] DeleteActionLogRequest request)
    {
        return await _repository.DeleteActionLogsAsync(request.Ids);
    }

    [HttpPost]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<ActionLogDto> Add([FromBody] AddActionLogRequest request)
    {
        var actionLog = _domainService.AddActionLog(request);
        var actionLogFromDb = await _dbContext.AddAsync(actionLog);
        await _dbContext.SaveChangesAsync();
        return actionLogFromDb.Entity.Adapt<ActionLogDto>();
    }
}