using Dotnet9.Privacies;
using System.Threading.Tasks;

namespace Dotnet9.Blazor.Pages.Admin;

public partial class Privacy
{
    private readonly IPrivacyAppService _privacyAppService;
    private string _markdownValue;
    private string _markdownHtml;

    public Privacy(IPrivacyAppService privacyAppService)
    {
        _privacyAppService = privacyAppService;
    }

    protected override async Task OnInitializedAsync()
    {
        var about = await _privacyAppService.GetAsync();
        _markdownValue = about.Details;
        _markdownHtml = Markdig.Markdown.ToHtml(_markdownValue ?? string.Empty);
    }

    private async Task OnMarkdownValueChanged(string value)
    {
        _markdownValue = value;
        _markdownHtml = Markdig.Markdown.ToHtml(_markdownValue ?? string.Empty);
        await _privacyAppService.UpdateAsync(new UpdatePrivacyDto { Details = _markdownValue });
    }
}