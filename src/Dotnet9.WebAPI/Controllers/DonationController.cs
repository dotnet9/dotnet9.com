namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DonationController : ControllerBase
{
    private readonly IMemoryCacheHelper _cacheHelper;
    private readonly Dotnet9DbContext _dbContext;
    private readonly DonationManager _manager;
    private readonly IDonationRepository _repository;
    private const string GetDonationCacheKey = "DonationController.GetDonation";

    public DonationController(Dotnet9DbContext dbContext, IDonationRepository repository, DonationManager manager,
        IMemoryCacheHelper cacheHelper)
    {
        _dbContext = dbContext;
        _repository = repository;
        _manager = manager;
        _cacheHelper = cacheHelper;
    }

    [HttpGet]
    [NoWrapper]
    public async Task<ActionResult<DonationDto?>> Get()
    {
        async Task<DonationDto?> GetDonationFromDb()
        {
            var dataFromDb = await _repository.GetAsync();
            return dataFromDb == null ? null : new DonationDto(dataFromDb.Content!);
        }

        var data = await _cacheHelper.GetOrCreateAsync(GetDonationCacheKey,
            async e => await GetDonationFromDb());
        if (data == null)
        {
            return NotFound();
        }

        return data;
    }

    [HttpPost]
    [Authorize(Roles = UserRoleConst.Admin)]
    public async Task<ActionResult<ResponseResult<bool>>> AddOrUpdate(AddOrUpdateDonationRequest request)
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
        _cacheHelper.Remove(GetDonationCacheKey);
        return ResponseResult<bool>.GetSuccess(true);
    }
}