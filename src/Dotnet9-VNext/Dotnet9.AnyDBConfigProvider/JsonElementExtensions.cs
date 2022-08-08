using System.Text.Json;

namespace Dotnet9.AnyDBConfigProvider;

internal static class JsonElementExtensions
{
    public static string? GetValueForConfig(this JsonElement e)
    {
        switch (e.ValueKind)
        {
            case JsonValueKind.String:
                //remove the quotes, "ab"-->ab
                return e.GetString();
            case JsonValueKind.Null:
            case JsonValueKind.Undefined:
                //remove the quotes, "null"-->null
                return null;
            default:
                return e.GetRawText();
        }
    }
}