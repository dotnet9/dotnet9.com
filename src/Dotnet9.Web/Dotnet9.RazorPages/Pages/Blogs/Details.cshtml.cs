namespace Dotnet9.RazorPages.Pages.Blogs
{
    public class DetailsModel : PageModel
    {
        [BindProperty(SupportsGet = true)] public string? Slug { get; set; }
        [BindProperty(SupportsGet = true)] public int Year { get; set; }
        [BindProperty(SupportsGet = true)] public int Month { get; set; }

        public BlogDetails? Blog { get; set; }
        public string? BlogContentHtml { get; set; }

        public async Task OnGet([FromServices] ICaller caller)
        {
            Blog = await caller.GetAsync<BlogDetails?>(
                $"/api/blogs/{Slug}");
            BlogContentHtml = Blog?.Content.Convert2Html();
        }
    }
}