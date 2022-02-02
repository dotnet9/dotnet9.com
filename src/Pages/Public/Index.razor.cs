using BlazorComponent;
using Dotnet9.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Dotnet9.Pages.Public;

public partial class Index
{
    private int _onboarding = 0;
    private int _length = 1;

    [CascadingParameter] public PublicLayout MainLayout { get; set; } = default!;

    [Inject] public IJSRuntime JSRuntime { get; set; } = default!;

    [Inject] public NavigationManager Navigation { get; set; } = default!;

    public StringNumber OnBoarding
    {
        get => _onboarding;
        set => _onboarding = value.AsT1;
    }

    private async Task Toggle(string url)
    {
        if (!string.IsNullOrWhiteSpace(url))
        {
            await JSRuntime.InvokeVoidAsync("window.open", url);
        }
    }

    public string? T(string key)
    {
        var content = MainLayout.T(key);
        return content;
    }
}