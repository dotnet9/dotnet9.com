namespace Dotnet9.AnyDBConfigProvider;

internal static class JsonElementExtensions
{
    public static string GetValueForConfig(this JsonElement e)
    {
        if (e.ValueKind == JsonValueKind.String)
        {
            //remove the quotes, "ab"-->ab
            return e.GetString();
        }

        if (e.ValueKind == JsonValueKind.Null
            || e.ValueKind == JsonValueKind.Undefined)
        {
            //remove the quotes, "null"-->null
            return null;
        }

        return e.GetRawText();
    }
}