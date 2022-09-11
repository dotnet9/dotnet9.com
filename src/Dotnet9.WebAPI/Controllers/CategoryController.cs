namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly Dotnet9DbContext _dbContext;
    private readonly CategoryManager _manager;
    private readonly ICategoryRepository _repository;

    public CategoryController(Dotnet9DbContext dbContext, ICategoryRepository repository,
        CategoryManager manager)
    {
        _dbContext = dbContext;
        _repository = repository;
        _manager = manager;
    }

    [HttpGet]
    public async Task<QueryCategoryResponse> List([FromQuery] QueryCategoryRequest request)
    {
        return await _repository.QueryAsync(request.Keywords, request.PageIndex, request.PageSize);
    }

    [HttpDelete]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<int> Delete([FromBody] DeleteCategoryRequest request)
    {
        return await _repository.DeleteAsync(request.Ids);
    }

    [HttpPost]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<CategoryDTO> Add([FromBody] AddCategoryRequest request)
    {
        var category = await _manager.CreateAsync(request);
        var categoryFromDb = await _dbContext.AddAsync(category);
        await _dbContext.SaveChangesAsync();
        return categoryFromDb.Entity.Adapt<CategoryDTO>();
    }

    [HttpPut]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<CategoryDTO> Update(Guid id, [FromBody] UpdateCategoryRequest request)
    {
        var category = await _manager.CreateAsync(id, request);
        var categoryFromDb = await _dbContext.AddAsync(category);
        await _dbContext.SaveChangesAsync();
        return categoryFromDb.Entity.Adapt<CategoryDTO>();
    }
}