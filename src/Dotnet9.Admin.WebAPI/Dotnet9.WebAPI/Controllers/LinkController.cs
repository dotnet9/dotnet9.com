namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LinkController : ControllerBase
{
    private readonly Dotnet9DbContext _dbContext;
    private readonly LinkManager _manager;
    private readonly ILinkRepository _repository;

    public LinkController(Dotnet9DbContext dbContext, ILinkRepository repository, LinkManager manager)
    {
        _dbContext = dbContext;
        _repository = repository;
        _manager = manager;
    }

    [HttpGet]
    [Authorize(Roles = UserRoleConst.Admin)]
    [NoWrapper]
    public async Task<GetLinkListResponse> List([FromQuery] GetLinkListRequest request)
    {
        var result = await _repository.GetListAsync(request);
        return new GetLinkListResponse(result.Links?.Adapt<LinkDto[]>(), result.Count, true, request.PageSize,
            request.Current);
    }

    [HttpDelete]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<int> Delete([FromBody] DeleteLinkRequest request)
    {
        return await _repository.DeleteAsync(request.Ids);
    }

    [HttpPost]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<LinkDto> Add([FromBody] AddLinkRequest request)
    {
        var data = await _manager.CreateAsync(null, request.SequenceNumber, request.Name, request.Url,
            request.Description, request.Kind);
        var dataFromDb = await _dbContext.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        return dataFromDb.Entity.Adapt<LinkDto>();
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<LinkDto> Update(Guid id, [FromBody] UpdateLinkRequest request)
    {
        var data = await _manager.CreateAsync(id, request.SequenceNumber, request.Name, request.Url,
            request.Description, request.Kind);
        var dataFromDb = _dbContext.Update(data);
        await _dbContext.SaveChangesAsync();
        return dataFromDb.Entity.Adapt<LinkDto>();
    }
}