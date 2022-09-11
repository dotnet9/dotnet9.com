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
    public async Task<QueryActionLogResponse> List([FromQuery] QueryActionLogRequest request)
    {
        return await _repository.QueryAsync(request.Keywords, request.PageIndex, request.PageSize);
    }

    [HttpDelete]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<int> Delete([FromBody] DeleteActionLogRequest request)
    {
        return await _repository.DeleteAsync(request.Ids);
    }

    [HttpPost]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<ActionLogDTO> Add([FromBody] AddActionLogRequest request)
    {
        var actionLog = _manager.Create(request);
        var actionLogFromDb = await _dbContext.AddAsync(actionLog);
        await _dbContext.SaveChangesAsync();
        return actionLogFromDb.Entity.Adapt<ActionLogDTO>();
    }
}