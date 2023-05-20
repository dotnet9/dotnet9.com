namespace Dotnet9.Caller.Services.Links;

public class LinkService : HttpClientCallerBase
{
    protected override string Prefix { get; set; } = "/api/links";
    protected override string BaseAddress { get; set; }

    public LinkService(IOptions<ServiceCallerOptions> option)
    {
        BaseAddress = option.Value.BaseAddress;
    }

    public async Task<List<FriendlyLinkDto>?> GetAllAsync(int page = 1, int pageSize = 100)
    {
        return await Caller.GetAsync<List<FriendlyLinkDto>>($"list?page={page}&pageSize={pageSize}");
    }
}