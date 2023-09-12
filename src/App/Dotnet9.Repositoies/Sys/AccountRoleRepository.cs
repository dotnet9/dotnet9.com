namespace Dotnet9.Repositoies.Sys;

public class AccountRoleRepository : BaseRepository<AccountRoles, Guid>
{
    public AccountRoleRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<AccountRoles>> GetAccountRoleListByRoleIds(List<Guid> roleIds)
    {
        return await Ctx.Set<AccountRoles>().Where(a => roleIds.Contains(a.Id))
            .ToListAsync();
    }

    public async Task AddRoleRelationAsync(Accounts account, List<AccountRoles> rolesList)
    {
        await Ctx.Set<AccountRoleRelation>().Where(a => a.Account == account).ExecuteDeleteAsync();
        account.AccountRoleRelations.AddRange(rolesList.Select(a => new AccountRoleRelation
        {
            AccountRole = a,
            Account = account
        }));
        await Ctx.SaveChangesAsync();
    }

    public async Task AddRole(string roleName)
    {
        bool any = await Ctx.Set<AccountRoles>().Where(a => a.RoleName == roleName).AnyAsync();
        if (any)
        {
            throw new UserException("已经存在");
        }

        await Ctx.Set<AccountRoles>().AddAsync(new AccountRoles
        {
            RoleName = roleName
        });
        await Ctx.SaveChangesAsync();
    }

    public async Task SetAccountToRole(Guid accountId, Guid roleId)
    {
        Accounts? account = await Ctx.Set<Accounts>().Where(a => a.Id == accountId).FirstOrDefaultAsync();
        AccountRoles? role = await Ctx.Set<AccountRoles>().FirstOrDefaultAsync(a => a.Id == roleId);
        if (role == null || account == null)
        {
            throw new UserException("操作失败！");
        }

        role.AccountRoleRelations.Add(new AccountRoleRelation
        {
            Account = account,
            AccountRole = role
        });
        await Ctx.SaveChangesAsync();
    }

    public async Task DelRole(Guid id)
    {
        var role = await Ctx.Set<AccountRoles>().Include(a => a.AccountRoleRelations).Where(a => a.Id == id)
            .Select(a => new
            {
                roleName = a.RoleName,
                count = a.AccountRoleRelations.Count()
            }).FirstOrDefaultAsync();
        if (role == null)
        {
            throw new UserException("删除失败");
        }

        if (role.count > 0)
        {
            throw new UserException("角色关联账户大于0");
        }

        await Ctx.Set<AccountRoles>().Where(a => a.Id == id).ExecuteDeleteAsync();
        await Ctx.SaveChangesAsync();
    }

    public async Task<PageDto<RoleListResponse>> GetRoleList(RoleListRequest request)
    {
        IQueryable<AccountRoles> query = Ctx.Set<AccountRoles>().AsQueryable();
        List<RoleListResponse> list = await Ctx.Set<AccountRoles>().Skip(request.Skip).Take(request.PageSize)
            .Select(a => new RoleListResponse
            {
                Id = a.Id,
                RoleName = a.RoleName,
                Count = a.AccountRoleRelations.Count
            }).ToListAsync();
        return new PageDto<RoleListResponse>(await query.CountAsync(), list);
    }
}