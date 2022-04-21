using Dotnet9.Core;
using Dotnet9.Domain.Users;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.Users;

public class EfCoreUserRepository : EfCoreRepository<User>, IUserRepository
{
    public EfCoreUserRepository(Dotnet9DbContext context) : base(context)
    {
    }

    public async Task<bool> ExistAdminAccountAsync()
    {
        return await DbContext.Set<User>().AnyAsync();
    }

    public async Task<int> InitAccountAsync(AdminAccountForCreation adminAccountForCreation)
    {
        if (await ExistAdminAccountAsync())
        {
            throw new AdminAccountAlreadyExistsException("系统已经初始化，重复操作失败");
        }

        await DbContext.Set<User>().AddAsync(new User()
        {
            Name="超级管理员",
            Account = adminAccountForCreation.Account,
            Password= IdentitySecurity.HashPassword(adminAccountForCreation.Password),
            Email=adminAccountForCreation.Email,
            CreateDate = DateTimeOffset.Now
        });
        return await DbContext.SaveChangesAsync();
    }
}