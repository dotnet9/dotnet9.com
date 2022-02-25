using BlazorComponent;
using BlazorComponent.I18n;
using Dotnet9.Tools.Images;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Dotnet9.Tools.Web.Pages.Public.ImageTools;

public partial class IcoTool
{
    private readonly List<Func<IBrowserFile, StringBoolean>> _rules = new();

    private bool _loading;
    private IBrowserFile? _sourceFile;
    [Inject] public I18n I18N { get; set; } = default!;
    [Inject] public IJSRuntime Js { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _rules.Add(value => (value == null || value.Size < 2 * 1024 * 1024 )? true : T("IcoToolFileSizeLimitMessage"));
        await base.OnInitializedAsync();
    }

    private void LoadFile(IBrowserFile? e)
    {
            _sourceFile = e;
    }

    private async Task ConvertToIcon()
    {
        if (_sourceFile == null)
        {
            return;
        }
        _loading = true;
        
        var fileName = $"{Path.GetFileNameWithoutExtension(_sourceFile.Name)}.ico";
        var inputStream = new MemoryStream();
        await _sourceFile.OpenReadStream().CopyToAsync(inputStream);
        var outputStream = new MemoryStream();
        ImagingHelper.ConvertToIcon(inputStream, outputStream);
        using var streamRef = new DotNetStreamReference(outputStream);
        await Js.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);

        _loading = false;
    }

    public string? T(string key)
    {
        return I18N.LanguageMap.GetValueOrDefault(key);
    }
}