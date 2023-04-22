namespace Dotnet9.RazorPages.Pages.Abouts
{
    public class IndexModel : PageModel
    {
        public string? AboutContentHtml { get; set; }

        public async Task OnGet([FromServices] ICaller caller)
        {
            var about = await caller.GetAsync<AboutDto?>($"/api/abouts");
            AboutContentHtml = about?.Content.Convert2Html();
        }
    }
}