using BlazorComponent.I18n;
using Microsoft.AspNetCore.Components;

namespace Dotnet9.Tools.Web.Shared;

public partial class PublicLayout
{
    private bool _drawer = true;
    public bool IsChinese { get; set; }

    [Inject] public I18n? I18N { get; set; }

    public void ChangeLanguage(string lang)
    {
        I18N?.SetLang(lang);
    }

    public string T(string key)
    {
        return I18N?.LanguageMap.GetValueOrDefault(key) ?? string.Empty;
    }
}