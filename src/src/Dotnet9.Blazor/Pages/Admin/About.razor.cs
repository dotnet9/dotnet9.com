using System.Threading.Tasks;
using Dotnet9.Abouts;

namespace Dotnet9.Blazor.Pages.Admin;

public partial class About
{
    private readonly IAboutAppService _aboutAppService;
    private string _markdownValue;

    public About(IAboutAppService aboutAppService)
    {
        _aboutAppService = aboutAppService;
    }

    protected override async void OnInitialized()
    {
        var about = await _aboutAppService.GetAsync();
        _markdownValue = about.Details;
        await base.OnInitializedAsync();
    }

    private async Task OnMarkdownValueChanged(string value)
    {
        //_markdownValue = value;
        //await _aboutAppService.UpdateAsync(new UpdateAboutDto() { Details = _markdownValue });
    }
}