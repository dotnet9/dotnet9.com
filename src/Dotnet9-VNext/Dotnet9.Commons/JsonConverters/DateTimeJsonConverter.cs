namespace Dotnet9.Commons.JsonConverters;

public class DateTimeJsonConverter : JsonConverter<DateTime>
{
    private readonly string _dateFormatString;

    public DateTimeJsonConverter()
    {
        _dateFormatString = "yyyy-MM-dd HH:mm:ss";
    }

    public DateTimeJsonConverter(string dateFormatString)
    {
        _dateFormatString = dateFormatString;
    }

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string? str = reader.GetString();
        return str == null ? default : DateTime.Parse(str);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_dateFormatString));
    }
}