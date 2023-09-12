using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Dotnet9Tools.Auth;

public class JwtHelper
{
    private readonly IConfiguration _configuration;

    public JwtHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetToken(Dictionary<string, object> payload, TimeSpan expTime)
    {
        string? key = _configuration.GetSection("Jwt:Secret").Value;
        if (string.IsNullOrWhiteSpace(key))
        {
            throw new ArgumentNullException("Jwt:Secret is Null");
        }

        SymmetricSecurityKey secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        // 3. 选择加密算法
        string? algorithm = SecurityAlgorithms.HmacSha256;
        // 4. 生成Credentials
        SigningCredentials signingCredentials = new SigningCredentials(secretKey, algorithm);
        // 5. 根据以上，生成token
        IEnumerable<Claim> claims = payload.Select(a => new Claim(a.Key, a.Value.ToString() ?? "_"));
        JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
            _configuration["Jwt:Issuer"], //Issuer
            _configuration["Jwt:Audience"], //Audience
            claims, //Claims,
            DateTime.Now, //notBefore
            DateTime.Now.Add(expTime), //expires
            signingCredentials //Credentials
        );
        // 6. 将token变为string
        string? token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

        return token;
    }
}

/// <summary>
///     jwt
/// </summary>
public static class JwtAuthExtensions
{
    /// <summary>
    ///     添加jwt身份验证
    /// </summary>
    /// <param name="service"></param>
    /// <param name="configuration"></param>
    public static void AddJwtAuth(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddScoped<JwtHelper>();
        service.AddAuthentication(options => { options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, //是否验证Issuer
                    ValidIssuer = configuration["Jwt:Issuer"], //发行人Issuer
                    ValidateAudience = true, //是否验证Audience
                    ValidAudience = configuration["Jwt:Audience"], //订阅人Audience
                    ValidateIssuerSigningKey = true, //是否验证SecurityKey
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secret"]!)), //SecurityKey
                    ValidateLifetime = true, //是否验证失效时间
                    ClockSkew = TimeSpan.FromSeconds(30), //过期时间容错值，解决服务器端时间不同步问题（秒）
                    RequireExpirationTime = true
                };
            });
    }
}