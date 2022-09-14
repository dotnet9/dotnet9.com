namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly Dotnet9DbContext _dbContext;
    private readonly TagManager _manager;
    private readonly ITagRepository _repository;

    public TagController(Dotnet9DbContext dbContext, ITagRepository repository, TagManager manager)
    {
        _dbContext = dbContext;
        _repository = repository;
        _manager = manager;
    }

    [HttpGet]
    public async Task<QueryTagResponse> List([FromQuery] QueryTagRequest request)
    {
        var result = await _repository.GetListAsync(request.Keywords, request.PageIndex, request.PageSize);
        return new QueryTagResponse(result.Tags?.Adapt<TagDto[]>(), result.Count);
    }

    [HttpDelete]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<int> Delete([FromBody] DeleteTagRequest request)
    {
        return await _repository.DeleteAsync(request.Ids);
    }

    [HttpPost]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<TagDto> Add([FromBody] AddTagRequest request)
    {
        var data = await _manager.CreateAsync(null, request.Name);
        var dataFromDb = await _dbContext.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        return dataFromDb.Entity.Adapt<TagDto>();
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<TagDto> Update(Guid id, [FromBody] UpdateTagRequest request)
    {
        var data = await _manager.CreateAsync(id, request.Name);
        var dataFromDb = _dbContext.Update(data);
        await _dbContext.SaveChangesAsync();
        return dataFromDb.Entity.Adapt<TagDto>();
    }
}