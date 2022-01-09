using Volo.Abp;

namespace Dotnet9.Albums;

public class AlbumAlreadyExistsException : BusinessException
{
    public AlbumAlreadyExistsException(string name)
        : base(Dotnet9DomainErrorCodes.Albums.AlbumAlreadyExist)
    {
        WithData("name", name);
    }
}