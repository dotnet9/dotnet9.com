using Volo.Abp;

namespace Dotnet9.Categories;

public class CategoryAlreadyExistsException : BusinessException
{
    public CategoryAlreadyExistsException(string name)
        : base(Dotnet9DomainErrorCodes.Categories.CagetoryAlreadyExist)
    {
        WithData("name", name);
    }
}