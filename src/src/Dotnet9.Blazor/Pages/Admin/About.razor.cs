using System.Threading.Tasks;
using Dotnet9.Abouts;

namespace Dotnet9.Blazor.Pages.Admin;

public partial class About
{
    private readonly IAboutAppService _aboutAppService;
    private string _markdownValue { get; set; }
    private string _markdownHtml;

    public About(IAboutAppService aboutAppService)
    {
        _aboutAppService = aboutAppService;
    }

    protected override async Task OnInitializedAsync()
    {
        var about = await _aboutAppService.GetAsync();
        _markdownValue = about.Details;
        _markdownHtml = Markdig.Markdown.ToHtml(_markdownValue ?? string.Empty);
    }
    async Task OnMarkdownValueChanged(string value)
    {
        _markdownValue = value;
        _markdownHtml = Markdig.Markdown.ToHtml(_markdownValue ?? string.Empty);
        await _aboutAppService.UpdateAsync(new UpdateAboutDto { Details=_markdownValue});
    }
}