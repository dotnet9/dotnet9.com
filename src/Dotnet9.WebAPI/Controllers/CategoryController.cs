using Microsoft.EntityFrameworkCore.ChangeTracking;

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
    [NoWrapper]
    public async Task<GetCategoryListResponse> List([FromQuery] GetCategoryListRequest request)
    {
        (Category[]? Categories, long Count) result = await _repository.GetListAsync(request);
        return new GetCategoryListResponse(result.Categories?.Adapt<CategoryDto[]>(), result.Count);
    }

    [HttpGet]
    [Route("/api/[controller]/names")]
    public async Task<string[]> GetCategoryNames()
    {
        return await _dbContext.Categories!.Select(x => x.Name).ToArrayAsync();
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
        Category category = await _manager.CreateAsync(null, request.SequenceNumber, request.Name, request.Slug,
            request.Cover, request.Description, request.Visible, request.ParentId);
        EntityEntry<Category> categoryFromDb = await _dbContext.AddAsync(category);
        await _dbContext.SaveChangesAsync();
        return categoryFromDb.Entity.Adapt<CategoryDto>();
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<CategoryDto> Update(Guid id, [FromBody] UpdateCategoryRequest request)
    {
        Category category = await _manager.CreateAsync(id, request.SequenceNumber, request.Name, request.Slug,
            request.Cover, request.Description, request.Visible, request.ParentId);
        EntityEntry<Category> categoryFromDb = _dbContext.Update(category);
        await _dbContext.SaveChangesAsync();
        return categoryFromDb.Entity.Adapt<CategoryDto>();
    }
}