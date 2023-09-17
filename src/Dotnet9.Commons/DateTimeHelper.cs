namespace Dotnet9.Commons;

public static class DateTimeHelper
{
    private static readonly DateTimeOffset TimestampPoint = new(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);

    public static long GetTimestamp(this DateTimeOffset date, TimestampKind kind = TimestampKind.Seconds)
    {
        var time = date.Subtract(TimestampPoint);
        return (long)(kind == TimestampKind.Seconds ? time.TotalSeconds : time.TotalMilliseconds);
    }

    public static DateTimeOffset ConvertToDate(this long timestamp, TimestampKind kind = TimestampKind.Seconds)
    {
        var time = kind == TimestampKind.Seconds
            ? TimestampPoint.AddSeconds(timestamp)
            : TimestampPoint.AddMilliseconds(timestamp);

        return time;
    }
}

public enum TimestampKind
{
    Seconds,
    Milliseconds
}