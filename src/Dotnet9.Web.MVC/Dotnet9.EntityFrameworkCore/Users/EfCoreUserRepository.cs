using Dotnet9.Core;
using Dotnet9.Domain.Users;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.Users;

public class EfCoreUserRepository : EfCoreRepository<CustomUser>, IUserRepository
{
    public EfCoreUserRepository(Dotnet9DbContext context) : base(context)
    {
    }

    public async Task<bool> ExistAdminAccountAsync()
    {
        return await DbContext.Set<CustomUser>().AnyAsync();
    }

    public async Task<int> InitAccountAsync(AdminAccountForCreation adminAccountForCreation)
    {
        if (await ExistAdminAccountAsync()) throw new AdminAccountAlreadyExistsException("系统已经初始化，重复操作失败");

        await DbContext.Set<CustomUser>().AddAsync(new CustomUser
        {
            Name = "超级管理员",
            Account = adminAccountForCreation.Account,
            Password = IdentitySecurity.HashPassword(adminAccountForCreation.Password),
            Email = adminAccountForCreation.Email,
            CreateDate = DateTimeOffset.Now
        });
        return await DbContext.SaveChangesAsync();
    }

    public async Task LoginFailAsync(CustomUser user)
    {
        user.LoginFailCount += 1;
        if (user.LoginFailCount >= 5 && !IsLocked(user)) user.LockedDate = DateTimeOffset.Now.AddMinutes(15);

        await DbContext.SaveChangesAsync();
    }

    public async Task LoginSuccessAsync(CustomUser user)
    {
        user.LockedDate = DateTimeOffset.Now.AddSeconds(-1);
        user.LoginFailCount = 0;
        user.LastLoginDate = DateTimeOffset.Now;
        await DbContext.SaveChangesAsync();
    }

    private static bool IsLocked(CustomUser user)
    {
        return user.LockedDate > DateTimeOffset.Now;
    }
}