namespace Dotnet9.WebAPI.Domain.UserAdmin;

public interface IIdRepository
{
    /// <summary>
    ///     根据Id获取用户
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<User?> FindByIdAsync(Guid userId);

    /// <summary>
    ///     根据用户名获取用户
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    Task<User?> FindByNameAsync(string userName);

    /// <summary>
    ///     根据手机号获取用户
    /// </summary>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    Task<User?> FindByPhoneNumberAsync(string phoneNumber);

    /// <summary>
    ///     创建用户
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<IdentityResult> CreateAsync(User user, string password);

    /// <summary>
    ///     记录一次登录失败
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<IdentityResult> AccessFailedAsync(User user);

    /// <summary>
    ///     生成重置密码的令牌
    /// </summary>
    /// <param name="user"></param>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    Task<string> GenerateChangedPhoneNumberTokenAsync(User user, string phoneNumber);

    /// <summary>
    ///     修改手机号码
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="phoneNumber"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<SignInResult> ChangePhoneNumberAsync(Guid userId, string phoneNumber, string token);

    /// <summary>
    ///     修改密码
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<IdentityResult> ChangePasswordAsync(Guid userId, string password);

    /// <summary>
    ///     获取用户的角色
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<IList<string>> GetRolesAsync(User user);

    /// <summary>
    ///     把用户user加入角色role
    /// </summary>
    /// <param name="user"></param>
    /// <param name="roleName"></param>
    /// <returns></returns>
    Task<IdentityResult> AddToRolesAsync(User user, string roleName);

    /// <summary>
    ///     为了登录而检查用户名、密码是否正确
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <param name="lockoutOnFailure"></param>
    /// <returns></returns>
    Task<SignInResult> CheckForSignInAsync(User user, string password, bool lockoutOnFailure);

    /// <summary>
    ///     确认手机号
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task ConfirmPhoneNumberAsync(Guid userId);

    /// <summary>
    ///     修改手机号
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    Task UpdatePhoneNumberAsync(Guid userId, string phoneNumber);

    /// <summary>
    ///     软删除
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<IdentityResult> RemoveUserAsync(Guid userId);

    /// <summary>
    ///     添加管理员
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="phoneNumber"></param>
    /// <returns>返回值第三个是生成的密码</returns>
    Task<(IdentityResult, User?, string? password)> AddAdminUserAsync(string userName, string phoneNumber);

    /// <summary>
    ///     重置密码
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>返回值第三个是生成的密码</returns>
    Task<(IdentityResult, User?, string? password)> ResetPasswordAsync(Guid userId);
}