namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlbumController : ControllerBase
{
    private readonly Dotnet9DbContext _dbContext;
    private readonly AlbumManager _manager;
    private readonly IAlbumRepository _repository;

    public AlbumController(Dotnet9DbContext dbContext, IAlbumRepository repository,
        AlbumManager manager)
    {
        _dbContext = dbContext;
        _repository = repository;
        _manager = manager;
    }

    [HttpGet]
    public async Task<GetAlbumListResponse> List([FromQuery] GetAlbumListRequest request)
    {
        var result = await _repository.GetListAsync(request.Keywords, request.PageIndex, request.PageSize);
        return new GetAlbumListResponse(result.Albums.ConvertToAlbumDtoArray(), result.Count);
    }

    [HttpGet]
    [Route("/api/[controller]/Categories")]
    public async Task<CategoryDto[]> GetCategoriesOfAlbum()
    {
        var categories = await _repository.GetCategoriesOfAlbumAsync();
        return categories.Adapt<CategoryDto[]>();
    }


    [HttpGet]
    [Route("/api/Category/{categoryId}/Album")]
    public async Task<GetAlbumsByCategoryResponse> GetAlbumsByCategory(Guid categoryId,
        [FromQuery] GetAlbumsByCategoryRequest request)
    {
        var result = await _repository.GetAlbumsByCategoryAsync(categoryId, request.PageIndex, request.PageSize);
        return new GetAlbumsByCategoryResponse(result.Albums.ConvertToAlbumDtoArray(), result.Count);
    }

    [HttpDelete]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<int> Delete([FromBody] DeleteAlbumRequest request)
    {
        return await _repository.DeleteAsync(request.Ids);
    }

    [HttpPost]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<AlbumDto?> Add([FromBody] AddAlbumRequest request)
    {
        var album = await _manager.CreateAsync(null, request.CategoryIds, request.SequenceNumber, request.Name,
            request.Slug, request.Cover, request.Description, request.Visible);
        var albumFromDb = await _dbContext.AddAsync(album);
        await _dbContext.SaveChangesAsync();

        return albumFromDb.Entity.ConvertToAlbumDto();
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<AlbumDto?> Update(Guid id, [FromBody] UpdateAlbumRequest request)
    {
        var album = await _manager.CreateAsync(id, request.CategoryIds, request.SequenceNumber, request.Name,
            request.Slug, request.Cover, request.Description, request.Visible);
        var albumFromDb = _dbContext.Update(album);
        await _dbContext.SaveChangesAsync();

        return albumFromDb.Entity.ConvertToAlbumDto();
    }
}