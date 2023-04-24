namespace Dotnet9.Contracts.Dto;

public record SitemapInfo(string[] AlbumSlugs, string[] CategorySlugs, Dictionary<string, DateTime> Blogs);