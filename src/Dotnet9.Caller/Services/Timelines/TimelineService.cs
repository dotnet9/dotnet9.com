namespace Dotnet9.Caller.Services.Timelines;

public class TimelineService : HttpClientCallerBase
{
    protected override string Prefix { get; set; } = "/api/timelines";
    protected override string BaseAddress { get; set; }

    public TimelineService(IOptions<ServiceCallerOptions> option)
    {
        BaseAddress = option.Value.BaseAddress;
    }

    public async Task<List<TimelineDto>?> GetAllAsync()
    {
        return await Caller.GetAsync<List<TimelineDto>>("");
    }
}