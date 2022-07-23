using Dotnet9.Domain.Repositories;

namespace Dotnet9.Domain.Users;

public interface IUserRepository : IRepository<CustomUser>
{
    Task<bool> ExistAdminAccountAsync();

    Task<int> InitAccountAsync(AdminAccountForCreation adminAccountForCreation);

    Task LoginFailAsync(CustomUser user);

    Task LoginSuccessAsync(CustomUser user);
}