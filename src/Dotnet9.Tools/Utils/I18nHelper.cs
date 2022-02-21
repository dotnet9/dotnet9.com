using System.Globalization;
using System.Text.Json;
using BlazorComponent.I18n;

namespace Dotnet9.Tools.Utils;

public class I18nHelper
{
    public static void AddLang()
    {
        var content = File.ReadAllText("wwwroot/locale/languages.json");
        var languageDict = JsonSerializer.Deserialize<Dictionary<string, string[]>>(content);
        if (languageDict is not { Count: > 0 }) return;
        var languages = languageDict["SupportLanguages"];
        var defaultLanguage = CultureInfo.CurrentCulture.Name;

        foreach (var language in languages)
        {
            var languageContent = File.ReadAllText($"wwwroot/locale/{language}.json");

            var isDefaultLanguage = defaultLanguage == language;

            I18n.AddLang(language, JsonSerializer.Deserialize<Dictionary<string, string>>(languageContent),
                isDefaultLanguage);
        }
    }
}