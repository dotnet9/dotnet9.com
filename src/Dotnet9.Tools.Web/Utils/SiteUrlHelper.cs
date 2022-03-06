using System.Net;

namespace Dotnet9.Tools.Web.Utils;

public static class SiteUrlHelper
{
    public const string BaseUrl = "https://tool.dotnet9.com";
    public const string ToolsIndexUrl = "/tools";
    public const string ToolsIconUrl = "/ico";
    public const string ToolsTimestampUrl = "/timestamp";
    public static string ToolsIconCoverUrl = $"{UploadsUrl}/2022/02/cover_13.jpg";
    public static string ToolsTimestampCoverUrl = $"{UploadsUrl}/2022/02/cover_17.jpeg";

    public static string AlbumFullUrl => $"{BaseUrl}/album";

    public static string CategoryFullUrl => $"{BaseUrl}/cat";

    public static string TagFullUrl => $"{BaseUrl}/tag";

    public static string ToolsIndexFullUrl => $"{BaseUrl}{ToolsIndexUrl}";

    public static string ToolsIconFullUrl => $"{BaseUrl}{ToolsIconUrl}";

    public static string ToolsTimestampFullUrl => $"{BaseUrl}{ToolsTimestampUrl}";

    public static string UploadsUrl => $"{BaseUrl}/doc/blog_contents/uploads";

    public static string CheckOrCreateFullUrl(this string url)
    {
        return url.StartsWith("/") ? $"{BaseUrl}{url}" : url;
    }

    public static string GetPostUrl(this string slug)
    {
        return $"/post/{slug}";
    }

    public static string GetPostFullUrl(this string slug)
    {
        return $"{BaseUrl}/post/{slug}";
    }

    public static string GetAlbumUrl(this string slug)
    {
        return $"/album/{slug}";
    }

    public static string GetAlbumFullUrl(this string slug)
    {
        return $"{BaseUrl}/album/{slug}";
    }

    public static string GetCategoryUrl(this string slug)
    {
        return $"/cat/{slug}";
    }

    public static string GetCategoryFullUrl(this string slug)
    {
        return $"{BaseUrl}/cat/{slug}";
    }

    public static string GetTagUrl(this string name)
    {
        return $"/tag/{WebUtility.UrlEncode(name)}";
    }

    public static string GetTagFullUrl(this string name)
    {
        return $"{BaseUrl}/cat/{WebUtility.UrlEncode(name)}";
    }

    public static string GetQueryUrl(this string filter)
    {
        return $"/s?filter={filter}";
    }

    public static string GetQueryFullUrl(this string filter)
    {
        return $"{BaseUrl}/s?filter={filter}";
    }
}