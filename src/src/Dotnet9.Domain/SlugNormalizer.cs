using Slugify;
using Unidecode.NET;

namespace Dotnet9;

public static class SlugNormalizer
{
    private static readonly SlugHelper SlugHelper = new();

    public static string Normalize(string input)
    {
        return SlugHelper.GenerateSlug(input?.Unidecode());
    }
}