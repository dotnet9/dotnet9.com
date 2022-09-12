namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrivacyController : ControllerBase
{
    private readonly IMemoryCacheHelper _cacheHelper;
    private readonly Dotnet9DbContext _dbContext;
    private readonly PrivacyManager _manager;
    private readonly IPrivacyRepository _repository;

    public PrivacyController(Dotnet9DbContext dbContext, IPrivacyRepository repository, PrivacyManager manager,
        IMemoryCacheHelper cacheHelper)
    {
        _dbContext = dbContext;
        _repository = repository;
        _manager = manager;
        _cacheHelper = cacheHelper;
    }

    [HttpGet]
    public async Task<ActionResult<PrivacyDto?>> Get()
    {
        async Task<PrivacyDto?> GetPrivacyFromDb()
        {
            var dataFromDb = await _repository.GetAsync();
            return dataFromDb == null ? null : new PrivacyDto(dataFromDb.Content!);
        }

        var data = await _cacheHelper.GetOrCreateAsync("PrivacyController.GetPrivacy",
            async e => await GetPrivacyFromDb());
        if (data == null)
        {
            return NotFound();
        }

        return data;
    }

    [HttpPost]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<ActionResult> AddOrUpdate(AddOrUpdatePrivacyRequest request)
    {
        var data = await _repository.GetAsync();
        if (data == null)
        {
            data = _manager.Create(request.Content);
            await _dbContext.AddAsync(data);
        }
        else
        {
            data.ChangeContent(request.Content);
        }

        await _dbContext.SaveChangesAsync();
        return Ok();
    }
}