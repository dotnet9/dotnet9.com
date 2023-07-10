namespace Dotnet9.WebShare.Helpers;

public static class MarkupStringExtensions
{
    public static MarkupString ToMarkupString(this string value)
    {
        return new MarkupString(value);
    }
}