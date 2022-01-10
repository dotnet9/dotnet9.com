using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Dotnet9.UrlLinks;

public class UrlLinkManager : DomainService
{
    private readonly IUrlLinkRepository _urlLinkRepository;

    public UrlLinkManager(IUrlLinkRepository urlLinkRepository)
    {
        _urlLinkRepository = urlLinkRepository;
    }

    public async Task<UrlLink> CreateAsync(
        [NotNull] string name,
        [NotNull] string url,
        string description,
        int index = 1)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.NotNullOrWhiteSpace(url, nameof(url));

        var existingUrlLink = await _urlLinkRepository.FindByNameAsync(name);
        if (existingUrlLink != null)
        {
            throw new UrlLinkNameAlreadyExistsException(name);
        }

        existingUrlLink = await _urlLinkRepository.FindByUrlAsync(url);
        if (existingUrlLink != null)
        {
            throw new UrlLinkUrlAlreadyExistsException(url);
        }

        return new UrlLink(GuidGenerator.Create(), name, url, description, index);
    }

    public async Task ChangeNameAsync([NotNull] UrlLink urlLink, [NotNull] string newName)
    {
        Check.NotNull(urlLink, nameof(urlLink));
        Check.NotNullOrWhiteSpace(newName, nameof(newName));

        var existingUrlLink = await _urlLinkRepository.FindByNameAsync(newName);
        if (existingUrlLink != null && existingUrlLink.Id != urlLink.Id)
        {
            throw new UrlLinkNameAlreadyExistsException(newName);
        }

        urlLink.ChangeName(newName);
    }

    public async Task ChangeUrlAsync([NotNull] UrlLink urlLink, [NotNull] string newUrl)
    {
        Check.NotNull(urlLink, nameof(urlLink));
        Check.NotNullOrWhiteSpace(newUrl, nameof(newUrl));

        var existingUrlLink = await _urlLinkRepository.FindByUrlAsync(newUrl);
        if (existingUrlLink != null && existingUrlLink.Id != urlLink.Id)
        {
            throw new UrlLinkUrlAlreadyExistsException(newUrl);
        }

        urlLink.ChangeUrl(newUrl);
    }
}