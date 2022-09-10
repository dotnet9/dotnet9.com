namespace Dotnet9.AnyDBConfigProvider;

internal static class JsonElementExtensions
{
    public static string? GetValueForConfig(this JsonElement e)
    {
        return e.ValueKind switch
        {
            JsonValueKind.String => e.GetString(),
            JsonValueKind.Null or JsonValueKind.Undefined => null,
            _ => e.GetRawText()
        };
    }
}