namespace Dotnet9.Web.Pages.Timelines;

public class IndexModel : PageModel
{
    private readonly IDistributedCacheHelper _cacheHelper;
    private readonly ITimelineRepository _repository;

    public IndexModel(ITimelineRepository repository,
        IDistributedCacheHelper cacheHelper)
    {
        _repository = repository;
        _cacheHelper = cacheHelper;
    }

    public List<TimelineDto>? Timelines { get; set; }

    public async Task OnGet()
    {
        const string cacheKey = "Timeline";

        async Task<List<TimelineDto>?> GetDataFromDb()
        {
            var data = await _repository.GetListAsync(new GetTimelineListRequest(string.Empty, 1, int.MaxValue));
            return data.Timelines?.Select(x => new TimelineDto(x.Id, x.Time, x.Title, x.Content)).ToList();
        }

        Timelines = await _cacheHelper.GetOrCreateAsync(cacheKey,
            async e => await GetDataFromDb());
    }
}