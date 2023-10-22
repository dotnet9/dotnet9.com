using Dotnet9.Application.Auth.Dtos;
using Dotnet9.Application.Config;

namespace Dotnet9.Application.Auth;

/// <summary>
/// 用户授权
/// </summary>
[AllowAnonymous]
public class AuthService : IDynamicApiController
{
    private readonly ISqlSugarRepository<SysUser> _sysUseRepository;
    private readonly CustomConfigService _customConfigService;
    private readonly ICaptcha _captcha;
    private readonly IIdGenerator _idGenerator;
    private readonly IEasyCachingProvider _easyCachingProvider;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthService(ISqlSugarRepository<SysUser> sysUseRepository,
        CustomConfigService customConfigService,
        ICaptcha captcha,
        IIdGenerator idGenerator,
        IEasyCachingProvider easyCachingProvider,
        IHttpContextAccessor httpContextAccessor)
    {
        _sysUseRepository = sysUseRepository;
        _customConfigService = customConfigService;
        _captcha = captcha;
        _idGenerator = idGenerator;
        _easyCachingProvider = easyCachingProvider;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 系统用户登录
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task SignIn(AdminLoginInput dto)
    {
        bool validate = _captcha.Validate(dto.Id, dto.Code);
        if (!validate)
        {
            throw Oops.Bah("验证码错误");
        }

        string signInErrorCacheKey = $"login.error.{dto.Account}";
        CacheValue<int> value = await _easyCachingProvider.GetAsync<int>(signInErrorCacheKey);
        var setting = await _customConfigService.Get<SysSecuritySetting>();
        //5分钟内连续验证密码失败超过4次将限制用户尝试
        if (value.HasValue && value.Value > (setting?.Times ?? 4))
        {
            throw Oops.Bah("由于您多次登录失败，系统已限制账户登录");
        }

        SysUser user = await _sysUseRepository.GetFirstAsync(x => x.Account == dto.Account);
        if (user == null)
        {
            throw Oops.Bah("用户名或密码错误");
        }

        var context = _httpContextAccessor.HttpContext;
        if (user.Status == AvailabilityStatus.Disable || (user.LockExpired.HasValue && DateTime.Now < user.LockExpired))
        {
            throw Oops.Bah("您的账号被锁定");
        }

        if (user.Password != MD5Encryption.Encrypt($"{_idGenerator.Encode(user.Id)}{dto.Password}"))
        {
            await _easyCachingProvider.SetAsync(signInErrorCacheKey, value.Value + 1, TimeSpan.FromMinutes(5));
            throw Oops.Bah("用户名或密码错误");
        }

        long uniqueId = _idGenerator.NewLong();
        string token = JWTEncryption.Encrypt(new Dictionary<string, object>()
        {
            [AuthClaimsConst.AuthIdKey] = user.Id,
            [AuthClaimsConst.AccountKey] = user.Account,
            [AuthClaimsConst.UuidKey] = uniqueId,
            [AuthClaimsConst.AuthPlatformTypeKey] = AuthPlatformType.Manager
        });
        // 获取刷新 token
        var refreshToken = JWTEncryption.GenerateRefreshToken(token);
        // 设置响应报文头
        context.SigninToSwagger(token);
        context!.Response.Headers["access-token"] = token;
        context.Response.Headers["x-access-token"] = refreshToken;
    }

    /// <summary>
    /// 获取验证码
    /// </summary>
    /// <param name="id">验证码唯一id</param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Captcha([FromQuery] string id)
    {
        var data = _captcha.Generate(id);
        var stream = new MemoryStream(data.Bytes);
        return new FileStreamResult(stream, "image/gif");
    }
}