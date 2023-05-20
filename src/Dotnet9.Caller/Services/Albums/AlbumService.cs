namespace Dotnet9.Caller.Services.Albums;

public class AlbumService : HttpClientCallerBase
{
    protected override string Prefix { get; set; } = "/api/albums";
    protected override string BaseAddress { get; set; }

    public AlbumService(IOptions<ServiceCallerOptions> option)
    {
        BaseAddress = option.Value.BaseAddress;
    }

    public async Task<List<AlbumBrief>?> GetBriefAsync()
    {
        return await Caller.GetAsync<List<AlbumBrief>>("brief");
    }

    public async Task<GetBlogListByAlbumSlugResponse?> GetBlogBriefListByAlbumSlugAsync(
        string slug, int pageSize = 10,
        int current = 1)
    {
        return await Caller.GetAsync<GetBlogListByAlbumSlugResponse>(
            $"{slug}/blogs?page={current}&pageSize={pageSize}");
    }
}