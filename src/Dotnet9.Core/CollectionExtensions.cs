namespace Dotnet9.Core;

public static class CollectionExtensions
{
    public static bool IsNullOrEmpty<T>(this ICollection<T>? source)
    {
        if (source != null) return source.Count <= 0;

        return true;
    }
}