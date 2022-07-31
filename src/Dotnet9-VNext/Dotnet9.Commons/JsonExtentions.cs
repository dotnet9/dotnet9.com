namespace Dotnet9.Commons;

public static class JsonExtentions
{
    public static readonly JavaScriptEncoder Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

    public static JsonSerializerOptions CreateJsonSerializerOptions(bool camelCase = false)
    {
        JsonSerializerOptions opt = new JsonSerializerOptions
        {
            Encoder = Encoder
        };
        if (camelCase)
        {
            opt.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            opt.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        }

        opt.Converters.Add(new DateTimeJsonConverter("yyyy-MM-dd HH:mm:ss"));
        return opt;
    }

    public static string ToJsonString(this object value, bool camelCase = false)
    {
        JsonSerializerOptions opt = CreateJsonSerializerOptions(camelCase);
        return JsonSerializer.Serialize(value, value.GetType(), opt);
    }

    public static T? ParseJson<T>(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return default;
        }

        JsonSerializerOptions opt = CreateJsonSerializerOptions();
        return JsonSerializer.Deserialize<T>(value, opt);
    }
}