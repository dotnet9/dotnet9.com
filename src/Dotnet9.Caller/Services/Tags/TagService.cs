namespace Dotnet9.Caller.Services.Tags;

public class TagService : HttpClientCallerBase
{
    protected override string Prefix { get; set; } = "/api/tags";
    protected override string BaseAddress { get; set; }

    public TagService(IOptions<ServiceCallerOptions> option)
    {
        BaseAddress = option.Value.BaseAddress;
    }

    public async Task<List<TagBrief>?> GetAllAsync()
    {
        return await Caller.GetAsync<List<TagBrief>>("");
    }

    public async Task<List<TagBrief>?> GetWeekTagsAsync()
    {
        return await Caller.GetAsync<List<TagBrief>>("week");
    }

    public async Task<GetBlogListByTagNameResponse?> GetBlogBriefListByTagNameAsync(
        string tagName, int pageSize = 10,
        int current = 1)
    {
        return await Caller.GetAsync<GetBlogListByTagNameResponse>(
            $"{WebUtility.UrlEncode(tagName)}/blogs?page={current}&pageSize={pageSize}");
    }
}