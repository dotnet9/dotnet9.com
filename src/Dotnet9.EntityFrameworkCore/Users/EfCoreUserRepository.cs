using Dotnet9.Domain.Users;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.Users;

public class EfCoreUserRepository : EfCoreRepository<User>, IUserRepository
{
    public EfCoreUserRepository(Dotnet9DbContext context) : base(context)
    {
    }
}