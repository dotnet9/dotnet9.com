namespace Dotnet9.Caller.Services.Categories;

public class CategoryService : HttpClientCallerBase
{
    protected override string Prefix { get; set; } = "/api/categories";
    protected override string BaseAddress { get; set; }

    public CategoryService(IOptions<ServiceCallerOptions> option)
    {
        BaseAddress = option.Value.BaseAddress;
    }

    public async Task<List<CategoryBrief>?> GetBriefAsync()
    {
        return await Caller.GetAsync<List<CategoryBrief>>("brief");
    }


    public async Task<GetBlogListByCategorySlugResponse?> GetBlogBriefListByCategorySlugAsync(
        string slug, int pageSize = 10,
        int current = 1)
    {
        return await Caller.GetAsync<GetBlogListByCategorySlugResponse>(
            $"{slug}/blogs?page={current}&pageSize={pageSize}");
    }
}