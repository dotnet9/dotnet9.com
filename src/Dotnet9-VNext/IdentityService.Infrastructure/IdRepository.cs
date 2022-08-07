using System.Text;

namespace IdentityService.Infrastructure;

internal class IdRepository : IIdRepository
{
    private readonly ILogger<IdRepository> _logger;
    private readonly RoleManager<Role> _roleManager;
    private readonly IdUserManager _userManager;

    public IdRepository(IdUserManager userManager, RoleManager<Role> roleManager, ILogger<IdRepository> logger)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    public Task<User?> FindByPhoneNumberAsync(string phoneNumber)
    {
        return _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
    }

    public Task<User?> FindByIdAsync(Guid userId)
    {
        return _userManager.FindByIdAsync(userId.ToString());
    }

    public Task<User?> FindByNameAsync(string userName)
    {
        return _userManager.FindByNameAsync(userName);
    }

    public Task<IdentityResult> CreateAsync(User user, string password)
    {
        return _userManager.CreateAsync(user, password);
    }

    public Task<IdentityResult> AccessFailedAsync(User user)
    {
        return _userManager.AccessFailedAsync(user);
    }

    public async Task<SignInResult> ChangePhoneNumberAsync(Guid userId, string phoneNumber, string token)
    {
        User? user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
        {
            throw new ArgumentException($"{userId}的用户不存在");
        }

        IdentityResult changeResult = await _userManager.ChangePhoneNumberAsync(user, phoneNumber, token);
        if (!changeResult.Succeeded)
        {
            await _userManager.AccessFailedAsync(user);
            string errorMsg = changeResult.Errors.SumErrors();
            _logger.LogWarning($"{phoneNumber}ChangePhoneNumberAsync失败，错误信息{errorMsg}");
            return SignInResult.Failed;
        }

        await ConfirmPhoneNumberAsync(user.Id);
        return SignInResult.Success;
    }

    public async Task<IdentityResult> ChangePasswordAsync(Guid userId, string password)
    {
        if (password.Length < 6)
        {
            IdentityError error = new()
            {
                Code = "Password Invalid",
                Description = "密码长度不能少于6"
            };
            return IdentityResult.Failed(error);
        }

        User? user = await _userManager.FindByIdAsync(userId.ToString());
        string token = await _userManager.GeneratePasswordResetTokenAsync(user!);
        IdentityResult resetPwdResult = await _userManager.ResetPasswordAsync(user, token, password);
        return resetPwdResult;
    }

    public Task<string> GenerateChangePhoneNumberTokenAsync(User user, string phoneNumber)
    {
        return _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
    }


    public Task<IList<string>> GetRolesAsync(User user)
    {
        return _userManager.GetRolesAsync(user);
    }

    public async Task<IdentityResult> AddToRoleAsync(User user, string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            Role role = new() { Name = roleName };
            IdentityResult result = await _roleManager.CreateAsync(role);
            if (result.Succeeded == false)
            {
                return result;
            }
        }

        return await _userManager.AddToRoleAsync(user, roleName);
    }

    public async Task<SignInResult> CheckForSignInAsync(User user, string password, bool lockoutOnFailure)
    {
        if (await _userManager.IsLockedOutAsync(user))
        {
            return SignInResult.LockedOut;
        }

        bool success = await _userManager.CheckPasswordAsync(user, password);
        if (success)
        {
            return SignInResult.Success;
        }

        if (!lockoutOnFailure)
        {
            return SignInResult.Failed;
        }

        IdentityResult r = await AccessFailedAsync(user);
        if (!r.Succeeded)
        {
            throw new ApplicationException("AccessFailed failed");
        }

        return SignInResult.Failed;
        ;
    }

    public async Task ConfirmPhoneNumberAsync(Guid id)
    {
        User? user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            throw new ArgumentException($"用户找不到，id={id}", nameof(id));
        }

        user.PhoneNumberConfirmed = true;
        await _userManager.UpdateAsync(user);
    }

    public async Task UpdatePhoneNumberAsync(Guid id, string phoneNumber)
    {
        User? user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            throw new ArgumentException($"用户找不到，id={id}", nameof(id));
        }

        user.PhoneNumber = phoneNumber;
        await _userManager.UpdateAsync(user);
    }

    public async Task<IdentityResult> RemoveUserAsync(Guid id)
    {
        User? user = await FindByIdAsync(id);
        IUserLoginStore<User> userLoginStore = _userManager.UserLoginStore;
        CancellationToken noneCT = default;
        IList<UserLoginInfo> logins = await userLoginStore.GetLoginsAsync(user!, noneCT);
        foreach (UserLoginInfo login in logins)
        {
            await userLoginStore.RemoveLoginAsync(user, login.LoginProvider, login.ProviderKey, noneCT);
        }

        user!.SoftDelete();
        IdentityResult result = await _userManager.UpdateAsync(user);
        return result;
    }

    public async Task<(IdentityResult, User?, string? password)> AddAdminUserAsync(string userName, string phoneNumber)
    {
        if (await FindByNameAsync(userName) != null)
        {
            return (ErrorResult($"已经存在用户名{userName}"), null, null);
        }

        if (await FindByPhoneNumberAsync(phoneNumber) != null)
        {
            return (ErrorResult($"已经存在手机号{phoneNumber}"), null, null);
        }

        User user = new(userName);
        user.PhoneNumber = phoneNumber;
        user.PhoneNumberConfirmed = true;
        string password = GeneratePassword();
        IdentityResult result = await CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return (result, null, null);
        }

        result = await AddToRoleAsync(user, "Admin");
        if (!result.Succeeded)
        {
            return (result, null, null);
        }

        return (IdentityResult.Success, user, password);
    }

    public async Task<(IdentityResult, User?, string? password)> ResetPasswordAsync(Guid id)
    {
        User? user = await FindByIdAsync(id);
        if (user == null)
        {
            return (ErrorResult("用户没找到"), null, null);
        }

        string password = GeneratePassword();
        string token = await _userManager.GeneratePasswordResetTokenAsync(user);
        IdentityResult result = await _userManager.ResetPasswordAsync(user, token, password);
        return !result.Succeeded ? (result, null, null) : (IdentityResult.Success, user, password);
    }

    private static IdentityResult ErrorResult(string msg)
    {
        IdentityError idError = new() { Description = msg };
        return IdentityResult.Failed(idError);
    }

    private string GeneratePassword()
    {
        PasswordOptions options = _userManager.Options.Password;
        int length = options.RequiredLength;
        bool nonAlphanumeric = options.RequireNonAlphanumeric;
        bool digit = options.RequireDigit;
        bool lowercase = options.RequireLowercase;
        bool uppercase = options.RequireUppercase;
        StringBuilder password = new();
        Random random = new();
        while (password.Length < length)
        {
            char c = (char)random.Next(32, 126);
            password.Append(c);
            if (char.IsDigit(c))
            {
                digit = false;
            }
            else if (char.IsLower(c))
            {
                lowercase = false;
            }
            else if (char.IsUpper(c))
            {
                uppercase = false;
            }
            else if (!char.IsLetterOrDigit(c))
            {
                nonAlphanumeric = false;
            }
        }

        if (nonAlphanumeric)
        {
            password.Append((char)random.Next(33, 48));
        }

        if (digit)
        {
            password.Append((char)random.Next(48, 58));
        }

        if (lowercase)
        {
            password.Append((char)random.Next(97, 123));
        }

        if (uppercase)
        {
            password.Append((char)random.Next(65, 91));
        }

        return password.ToString();
    }
}