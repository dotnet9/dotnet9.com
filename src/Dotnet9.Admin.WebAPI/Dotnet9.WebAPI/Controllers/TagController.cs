namespace Dotnet9.WebAPI.Controllers;

[Route("api/tags")]
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


    [HttpGet("topten")]
    public async Task<ActionResult<TagDto[]?>> TopTen() 
    {

        (TagDto[]? Tags, long Count) result = await _repository.GetListAsync(new GetTagListRequest(null,1,10));
        return result.Tags;
    }

    [HttpGet]
    public async Task<GetTagListResponse> List([FromQuery] GetTagListRequest request)
    {
        (TagDto[]? Tags, long Count) result = await _repository.GetListAsync(request);
        return new GetTagListResponse(result.Tags, result.Count);
    }

    [HttpGet]
    [Route("/api/tags/tree")]
    public async Task<TagListItemDto[]> GetTree()
    {
        return await _dbContext.Tags!.Select(x => new TagListItemDto(x.Name, x.Id.ToString(), x.Id.ToString()))
            .ToArrayAsync();
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
        Tag data = await _manager.CreateAsync(null, request.Name);
        EntityEntry<Tag> dataFromDb = await _dbContext.AddAsync(data);
        await _dbContext.SaveChangesAsync();
        return dataFromDb.Entity.Adapt<TagDto>();
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<TagDto> Update(Guid id, [FromBody] UpdateTagRequest request)
    {
        Tag data = await _manager.CreateAsync(id, request.Name);
        EntityEntry<Tag> dataFromDb = _dbContext.Update(data);
        await _dbContext.SaveChangesAsync();
        return dataFromDb.Entity.Adapt<TagDto>();
    }
}