using Dotnet9.Privacies;
using System.Threading.Tasks;

namespace Dotnet9.Blazor.Pages.Public;

public partial class Privacy
{
    private readonly IPrivacyAppService _privacyAppService;
    private string _markdownHtml;

    public Privacy(IPrivacyAppService privacyAppService)
    {
        _privacyAppService = privacyAppService;
    }

    protected override async Task OnInitializedAsync()
    {
        var privacy = await _privacyAppService.GetAsync();
        _markdownHtml = Markdig.Markdown.ToHtml(privacy.Details ?? string.Empty);
    }
}