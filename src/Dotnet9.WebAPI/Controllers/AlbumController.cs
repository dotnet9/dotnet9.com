using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlbumController : ControllerBase
{
    private readonly IMemoryCacheHelper _cacheHelper;
    private readonly Dotnet9DbContext _dbContext;
    private readonly AlbumManager _manager;
    private readonly IAlbumRepository _repository;
    private readonly IOptionsSnapshot<SiteOptions> _siteOptions;

    public AlbumController(Dotnet9DbContext dbContext, IAlbumRepository repository,
        AlbumManager manager, IOptionsSnapshot<SiteOptions> siteOptions,
        IMemoryCacheHelper cacheHelper)
    {
        _dbContext = dbContext;
        _repository = repository;
        _manager = manager;
        _siteOptions = siteOptions;
        _cacheHelper = cacheHelper;
    }

    [HttpGet]
    [NoWrapper]
    public async Task<GetAlbumListResponse> List([FromQuery] GetAlbumListRequest request)
    {
        (Album[]? Albums, long Count) result = await _repository.GetListAsync(request);
        Dictionary<Guid, string>? categoryIdAndNames = await GetCategoryIDAndNames();
        return new GetAlbumListResponse(
            result.Albums.ConvertToAlbumDtoArray(_siteOptions.Value.AssetsRemotePath, categoryIdAndNames),
            result.Count, true, request.PageSize,
            request.Current);
    }

    [HttpGet]
    [Route("/api/[controller]/Categories")]
    public async Task<CategoryDto[]> GetCategoriesOfAlbum()
    {
        Category[] categories = await _repository.GetCategoriesOfAlbumAsync();
        return categories.Adapt<CategoryDto[]>();
    }


    [HttpGet]
    [Route("/api/Category/{categoryId}/Album")]
    public async Task<GetAlbumsByCategoryResponse> GetAlbumsByCategory(Guid categoryId,
        [FromQuery] GetAlbumsByCategoryRequest request)
    {
        (Album[]? Albums, long Count) result =
            await _repository.GetAlbumsByCategoryAsync(categoryId, request.PageIndex, request.PageSize);

        Dictionary<Guid, string>? categoryIdAndNames = await GetCategoryIDAndNames();
        return new GetAlbumsByCategoryResponse(
            result.Albums.ConvertToAlbumDtoArray(_siteOptions.Value.AssetsRemotePath, categoryIdAndNames),
            result.Count);
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
        Album album = await _manager.CreateAsync(null, request.CategoryNames, request.SequenceNumber, request.Name,
            request.Slug, request.Cover, request.Description, request.Visible);
        EntityEntry<Album> albumFromDb = await _dbContext.AddAsync(album);
        await _dbContext.SaveChangesAsync();
        Dictionary<Guid, string>? categoryIdAndNames = await GetCategoryIDAndNames();

        return albumFromDb.Entity.ConvertToAlbumDto(_siteOptions.Value.AssetsRemotePath, categoryIdAndNames);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<AlbumDto?> Update(Guid id, [FromBody] UpdateAlbumRequest request)
    {
        Album album = await _manager.CreateAsync(id, request.CategoryNames, request.SequenceNumber, request.Name,
            request.Slug, request.Cover, request.Description, request.Visible);
        EntityEntry<Album> albumFromDb = _dbContext.Update(album);
        await _dbContext.SaveChangesAsync();
        Dictionary<Guid, string>? categoryIdAndNames = await GetCategoryIDAndNames();

        return albumFromDb.Entity.ConvertToAlbumDto(_siteOptions.Value.AssetsRemotePath, categoryIdAndNames);
    }

    [HttpPut]
    [Route("/api/[controller]/{id}/changeVisible")]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<ResponseResult<AlbumDto?>> UpdateAlbumVisible(Guid id,
        [FromBody] UpdateAlbumVisibleRequest request)
    {
        Album album = await _manager.ChangeVisible(id, request.Visible);

        await _dbContext.SaveChangesAsync();

        Dictionary<Guid, string>? categoryIdAndNames = await GetCategoryIDAndNames();
        return album.ConvertToAlbumDto(_siteOptions.Value.AssetsRemotePath, categoryIdAndNames);
    }

    private async Task<Dictionary<Guid, string>?> GetCategoryIDAndNames()
    {
        async Task<Dictionary<Guid, string>?> GetIdAndNamesFromDb()
        {
            return await _dbContext.Categories!.ToDictionaryAsync(category => category.Id, category => category.Name);
        }

        return await _cacheHelper.GetOrCreateAsync("CategoryIDAndNames", async e => await GetIdAndNamesFromDb());
    }
}