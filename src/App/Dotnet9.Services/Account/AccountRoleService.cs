using Dotnet9.Repositoies.Sys;

namespace Dotnet9.Services.Sys;

public class AccountRoleService
{
    private readonly AccountRoleRepository _accountRoleRepository;

    public AccountRoleService(AccountRoleRepository accountRoleRepository)
    {
        _accountRoleRepository = accountRoleRepository;
    }
}