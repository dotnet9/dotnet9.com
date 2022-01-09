using Volo.Abp;

namespace Dotnet9.UrlLinks;

public class UrlLinkNameAlreadyExistsException : BusinessException
{
    public UrlLinkNameAlreadyExistsException(string name)
        : base(Dotnet9DomainErrorCodes.UrlLinks.NameAlreadyExist)
    {
        WithData("name", name);
    }
}