using BlazorComponent;
using BlazorComponent.I18n;
using Dotnet9.Tools.Images;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Dotnet9.Tools.Web.Pages.Public.ImageTools;

public partial class IcoTool
{
    private static readonly string ImageDirName = "files";

    private readonly List<Func<IBrowserFile, StringBoolean>> _rules = new();
    private string _destFilePath = "";

    private bool _loading;
    private string _sourceFilePath = "";
    [Inject] public I18n I18N { get; set; } = default!;
    [Inject] public IJSRuntime Js { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _rules.Add(value => value.Size < 2 * 1024 * 1024 ? true : T("IcoToolFileSizeLimitMessage"));
        await base.OnInitializedAsync();
    }

    private async Task LoadFile(InputFileChangeEventArgs e)
    {
        _destFilePath = string.Empty;
        if (!string.IsNullOrWhiteSpace(_sourceFilePath) && File.Exists(_sourceFilePath)) File.Delete(_sourceFilePath);

        var saveImageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "wwwroot", ImageDirName);
        if (!Directory.Exists(saveImageDir)) Directory.CreateDirectory(saveImageDir);

        _sourceFilePath = Path.Combine(saveImageDir, DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"));
        await using var fs = new FileStream(_sourceFilePath, FileMode.Create);
        await e.File.OpenReadStream().CopyToAsync(fs);
    }

    private async Task ConvertToIcon()
    {
        if (!string.IsNullOrWhiteSpace(_destFilePath) && File.Exists(_destFilePath))
        {
            await DownloadIco();
            return;
        }

        _loading = true;

        if (!string.IsNullOrWhiteSpace(_sourceFilePath) && File.Exists(_sourceFilePath))
        {
            _destFilePath = $"{_sourceFilePath}.ico";
            if (ImagingHelper.ConvertToIcon(_sourceFilePath, _destFilePath)) await DownloadIco();
        }

        _loading = false;
    }

    private async Task DownloadIco()
    {
        await using var fileStream = new FileStream(_destFilePath, FileMode.Open);
        using var streamRef = new DotNetStreamReference(fileStream);

        await Js.InvokeVoidAsync("downloadFileFromStream", Path.GetFileName(_destFilePath), streamRef);
    }


    public string? T(string key)
    {
        return I18N.LanguageMap.GetValueOrDefault(key);
    }
}