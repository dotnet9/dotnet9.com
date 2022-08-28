using Dotnet9.Core;

namespace Dotnet9.Domain.UrlLinks;

public class UrlLinkManager
{
    private readonly IUrlLinkRepository _urlLinkRepository;

    public UrlLinkManager(IUrlLinkRepository urlLinkRepository)
    {
        _urlLinkRepository = urlLinkRepository;
    }

    public async Task<UrlLink> CreateAsync(int? id, int index, UrlLinkKind kind, string name, string? description,
        string url)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(url, nameof(url));

        var existUrlLink = await _urlLinkRepository.FindByNameAsync(name);
        if (existUrlLink != null)
        {
            throw new Exception($"存在同名的链接: {name}");
        }

        existUrlLink = await _urlLinkRepository.FindByUrlAsync(url);
        if (existUrlLink != null)
        {
            throw new Exception($"存在相同Url的链接: {url}");
        }

        if (id != null)
        {
            return new UrlLink(id.Value, index, kind, name, description, url);
        }

        var maxIdOfUrlLink = await _urlLinkRepository.GetMaxIdAsync();
        id = maxIdOfUrlLink + 1;

        return new UrlLink(id.Value, index, kind, name, description, url);
    }

    public async Task ChangeNameAsync(UrlLink urlLink, string newName)
    {
        Check.NotNull(urlLink, nameof(urlLink));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existUrlLink = await _urlLinkRepository.FindByNameAsync(newName);
        if (existUrlLink != null && existUrlLink.Id != urlLink.Id)
        {
            throw new Exception("存在同名的链接");
        }

        urlLink.ChangeName(newName);
    }

    public async Task ChangeUrlAsync(UrlLink urlLink, string newUrl)
    {
        Check.NotNull(urlLink, nameof(urlLink));
        Check.NotNullOrWhiteSpace(newUrl, nameof(newUrl));

        var existUrlLink = await _urlLinkRepository.FindByUrlAsync(newUrl);
        if (existUrlLink != null && existUrlLink.Id != urlLink.Id)
        {
            throw new Exception("存在相同Url的链接");
        }

        urlLink.ChangeUrl(newUrl);
    }
}