using Volo.Abp;

namespace Dotnet9.UrlLinks;

public class UrlLinkUrlAlreadyExistsException : BusinessException
{
    public UrlLinkUrlAlreadyExistsException(string url)
        : base(Dotnet9DomainErrorCodes.UrlLinks.UrlAlreadyExist)
    {
        WithData("url", url);
    }
}