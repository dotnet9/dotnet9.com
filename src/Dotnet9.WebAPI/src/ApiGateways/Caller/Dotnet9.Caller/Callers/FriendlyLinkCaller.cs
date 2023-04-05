namespace Dotnet9.Caller.Callers;

public class FriendlyLinkCaller : HttpClientCallerBase
{
    protected override string BaseAddress { get; set; } = "http://localhost:5001";

    public FriendlyLinkCaller(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public async Task<List<FriendlyLinkDto>> GetListAsync()
    {
        return await GetCaller().GetAsync<List<FriendlyLinkDto>>("friendlylink/list");
    }
}