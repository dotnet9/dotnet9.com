using System.Threading.Tasks;
using Dotnet9.Abouts;

namespace Dotnet9.Blazor.Pages.Public;

public partial class About
{
    private readonly IAboutAppService _aboutAppService;
    private string _markdownHtml;

    public About(IAboutAppService aboutAppService)
    {
        _aboutAppService = aboutAppService;
    }

    protected override async Task OnInitializedAsync()
    {
        var about = await _aboutAppService.GetAsync();
        _markdownHtml = Markdig.Markdown.ToHtml(about.Details ?? string.Empty);
    }
}