namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IAlbumRepository _albumRepository;
    private readonly Dotnet9DbContext _dbContext;
    private readonly CategoryManager _manager;
    private readonly ICategoryRepository _repository;

    public CategoryController(Dotnet9DbContext dbContext, ICategoryRepository repository,
        IAlbumRepository albumRepository,
        CategoryManager manager)
    {
        _dbContext = dbContext;
        _repository = repository;
        _albumRepository = albumRepository;
        _manager = manager;
    }

    [HttpGet]
    public async Task<QueryCategoryResponse> List([FromQuery] QueryCategoryRequest request)
    {
        var result = await _repository.GetListAsync(request.Keywords, request.PageIndex, request.PageSize);
        return new QueryCategoryResponse(result.Categories?.Adapt<CategoryDto[]>(), result.Count);
    }

    [HttpDelete]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<int> Delete([FromBody] DeleteCategoryRequest request)
    {
        return await _repository.DeleteAsync(request.Ids);
    }

    [HttpPost]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<CategoryDto> Add([FromBody] AddCategoryRequest request)
    {
        var category = await _manager.CreateAsync(null, request.SequenceNumber, request.Name, request.Slug,
            request.Cover, request.Description, request.Visible, request.ParentId);
        var categoryFromDb = await _dbContext.AddAsync(category);
        await _dbContext.SaveChangesAsync();
        return categoryFromDb.Entity.Adapt<CategoryDto>();
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<CategoryDto> Update(Guid id, [FromBody] UpdateCategoryRequest request)
    {
        var category = await _manager.CreateAsync(id, request.SequenceNumber, request.Name, request.Slug,
            request.Cover, request.Description, request.Visible, request.ParentId);
        var categoryFromDb = _dbContext.Update(category);
        await _dbContext.SaveChangesAsync();
        return categoryFromDb.Entity.Adapt<CategoryDto>();
    }
}