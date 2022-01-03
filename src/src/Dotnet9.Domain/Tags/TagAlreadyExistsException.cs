using Volo.Abp;

namespace Dotnet9.Tags;

public class TagAlreadyExistsException : BusinessException
{
    public TagAlreadyExistsException(string name)
        : base(Dotnet9DomainErrorCodes.Tags.TagAlreadyExist)
    {
        WithData("name", name);
    }
}