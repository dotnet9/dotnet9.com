namespace System;

public static class JsonExtentions
{
    //如果不设置这个，那么"雅思真题"就会保存为"\u96C5\u601D\u771F\u9898"
    public static readonly JavaScriptEncoder Encoder = JavaScriptEncoder.Create(UnicodeRanges.All);

    public static JsonSerializerOptions CreateJsonSerializerOptions(bool camelCase = false)
    {
        JsonSerializerOptions opt = new JsonSerializerOptions { Encoder = Encoder };
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

    public static T? ParseJson<T>(this string value, bool camelCase = false)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return default;
        }

        JsonSerializerOptions opt = CreateJsonSerializerOptions(camelCase);
        return JsonSerializer.Deserialize<T>(value, opt);
    }
}