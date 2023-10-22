namespace Dotnet9.Application.Auth;

/// <summary>
/// 用户授权信息
/// </summary>
public class AuthManager : ITransient
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthManager(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 用户Id
    /// </summary>
    public long UserId =>
        _httpContextAccessor.HttpContext!.User.FindFirst(AuthClaimsConst.AuthIdKey)?.Value.Adapt<long>() ?? 0;

    /// <summary>
    /// 是否是超级管理员
    /// </summary>
    public bool IsSuperAdmin => UserId == 1;

    /// <summary>
    /// 登录名
    /// </summary>
    public string Account => _httpContextAccessor.HttpContext!.User.FindFirst(AuthClaimsConst.AccountKey)!.Value;

    /// <summary>
    /// 登录唯一Id
    /// </summary>
    public long UniqueId =>
        _httpContextAccessor.HttpContext!.User.FindFirst(AuthClaimsConst.UuidKey)!.Value.Adapt<long>();

    /// <summary>
    /// 授权平台类型
    /// </summary>
    public AuthPlatformType? AuthPlatformType => _httpContextAccessor.HttpContext!.User
        .FindFirst(AuthClaimsConst.AuthPlatformTypeKey)?.Value.Adapt<AuthPlatformType>();
}