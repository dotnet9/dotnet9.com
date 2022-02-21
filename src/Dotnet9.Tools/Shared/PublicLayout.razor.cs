using System.Globalization;
using BlazorComponent;
using BlazorComponent.I18n;
using Dotnet9.Tools.Utils;
using Microsoft.AspNetCore.Components;

namespace Dotnet9.Tools.Shared;

public partial class PublicLayout
{
    private readonly bool _isShowMiniLogo = true;
    private string? _languageIcon;
    private string _searchBorderColor = "#00000000";

    [Parameter] public bool IsChinese { get; set; }

    public StringNumber SelectTab { get; set; } = 0;

    [Inject] public I18n I18n { get; set; } = default!;

    [Inject] public NavigationManager Navigation { get; set; } = default!;

    [Inject] public GlobalConfigs GlobalConfig { get; set; } = default!;

    [Parameter] public bool Drawer { get; set; } = true;

    [Parameter] public bool Temporary { get; set; } = true;

    public void UpdateNav(bool drawer, bool temporary = true)
    {
        Drawer = drawer;
        Temporary = temporary;

        InvokeAsync(StateHasChanged);
    }

    private void TurnLanguage()
    {
        IsChinese = !IsChinese;
        var lang = IsChinese ? "zh-CN" : "en-US";

        ChangeLanguage(lang);

        GlobalConfig.Language = lang;
        GlobalConfig.SaveChanges();
    }

    private void ChangeLanguage(string lang)
    {
        _languageIcon = $"{lang}.png";
        I18n.SetLang(lang);
    }

    protected override void OnInitialized()
    {
        string lang;
        if (GlobalConfig.Language != null)
            lang = GlobalConfig.Language;
        else if (GlobalConfigs.StaticLanguage != null)
            lang = GlobalConfigs.StaticLanguage;
        else
            lang = CultureInfo.CurrentCulture.Name;


        IsChinese = lang == "zh-CN";

        ChangeLanguage(lang);
    }


    private void ShowDraw()
    {
        UpdateNav(true);
    }

    public string? T(string key)
    {
        return I18n.LanguageMap.GetValueOrDefault(key);
    }
}