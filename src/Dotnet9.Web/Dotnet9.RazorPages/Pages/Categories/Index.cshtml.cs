namespace Dotnet9.RazorPages.Pages.Categories
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)] public string? Slug { get; set; }
        [BindProperty(SupportsGet = true)] public int Current { get; set; } = 1;
        [BindProperty(SupportsGet = true)] public int PageSize { get; set; } = 10;

        public GetBlogListByCategorySlugResponse? RequestResponse { get; set; }
        public int[]? Pages { get; set; }

        public List<BlogBrief>? Blogs { get; set; }

        public async Task OnGet([FromServices] ICaller caller)
        {
            RequestResponse = await caller.GetAsync<GetBlogListByCategorySlugResponse>(
                $"/api/categories/{Slug}/blogs?page={Current}&pageSize={PageSize}");

            Blogs = RequestResponse?.Records;
            Pages = Enumerable.Range(1, RequestResponse!.TotalPage).ToArray();
        }
    }
}