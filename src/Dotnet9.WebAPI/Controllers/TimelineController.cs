namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TimelineController : ControllerBase
{
    private readonly Dotnet9DbContext _dbContext;
    private readonly TimelineManager _manager;
    private readonly ITimelineRepository _repository;

    public TimelineController(Dotnet9DbContext dbContext, ITimelineRepository repository, TimelineManager manager)
    {
        _dbContext = dbContext;
        _repository = repository;
        _manager = manager;
    }

    [HttpGet]
    [NoWrapper]
    public async Task<GetTimelineListResponse> List([FromQuery] GetTimelineListRequest request)
    {
        var result = await _repository.GetListAsync(request);
        return new GetTimelineListResponse(result.Timelines?.Adapt<TimelineDto[]>(), result.Count, true,
            request.PageSize, request.Current);
    }

    [HttpDelete]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<int> Delete([FromBody] DeleteTimelineRequest request)
    {
        return await _repository.DeleteAsync(request.Ids);
    }

    [HttpPost]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<TimelineDto> Add([FromBody] AddTimelineRequest request)
    {
        var data = await _manager.CreateAsync(null, request.Time, request.Title, request.Content);
        var dataFromDb = await _dbContext.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        return dataFromDb.Entity.Adapt<TimelineDto>();
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<TimelineDto> Update(Guid id, [FromBody] UpdateTimelineRequest request)
    {
        var data = await _manager.CreateAsync(id, request.Time, request.Title, request.Content);
        var dataFromDb = _dbContext.Update(data);
        await _dbContext.SaveChangesAsync();
        return dataFromDb.Entity.Adapt<TimelineDto>();
    }
}