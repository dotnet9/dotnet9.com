using BlazorComponent;
using Dotnet9.Tools.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Dotnet9.Tools.Pages.Public;

public partial class Index
{
    private readonly int _length = 1;
    private int _onboarding;

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
        if (!string.IsNullOrWhiteSpace(url)) await JSRuntime.InvokeVoidAsync("window.open", url);
    }

    public string? T(string key)
    {
        var content = MainLayout.T(key);
        return content;
    }
}