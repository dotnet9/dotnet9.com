using Dotnet9.Domain.Repositories;

namespace Dotnet9.Domain.Users;

public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistAdminAccountAsync();

    Task<int> InitAccountAsync(AdminAccountForCreation adminAccountForCreation);
}