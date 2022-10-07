namespace Dotnet9.Core;

public static class DateTimeOffsetHelper
{
    public static string? DateTimeToString(DateTimeOffset? date)
    {
        return date?.AddHours(8).ToString("yyyy-MM-dd HH:mm:ss");
    }
}