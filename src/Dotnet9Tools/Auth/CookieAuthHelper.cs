using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet9Tools.Auth;

public class CookieAuthHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieAuthHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task CookieLogin(List<Claim> claims)
    {
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);
        AuthenticationProperties authProperties = new AuthenticationProperties
        {
            AllowRefresh = true,
            // Refreshing the authentication session should be allowed.

            //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            // The time at which the authentication ticket expires. A 
            // value set here overrides the ExpireTimeSpan option of 
            // CookieAuthenticationOptions set with AddCookie.

            IsPersistent = true
            // Whether the authentication session is persisted across 
            // multiple requests. When used with cookies, controls
            // whether the cookie's lifetime is absolute (matching the
            // lifetime of the authentication ticket) or session-based.

            //IssuedUtc = <DateTimeOffset>,
            // The time at which the authentication ticket was issued.

            //RedirectUri = <string>
            // The full path or absolute URI to be used as an http 
            // redirect response value.
        };
        await _httpContextAccessor.HttpContext!.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }

    public async Task CookieSignOutAsync()
    {
        await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}

public static class CookieAuthExtensions
{
    public static void AddCookieAuth(this IServiceCollection service,
        Action<CookieAuthenticationOptions>? action = null)
    {
        service.AddScoped<CookieAuthHelper>();
        service.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(
                opt =>
                {
                    opt.Cookie = new CookieBuilder
                    {
                        HttpOnly = true,
                        SameSite = SameSiteMode.Strict,
                        Name = "Dotnet9.Blog"
                    };
                    opt.Events.OnRedirectToLogin = async context =>
                    {
                        string contentType = context.Request.Headers.Accept.ToString();
                        context.Response.StatusCode = 401;
                        if (contentType.Contains("json"))
                        {
                        }
                        else
                        {
                            await context.Response.WriteAsync("401 未授权");
                        }
                    };
                    action?.Invoke(opt);
                });
    }

    public static void UseCookieAuth(this IApplicationBuilder app)
    {
        app.UseCookiePolicy(new CookiePolicyOptions
        {
            MinimumSameSitePolicy = SameSiteMode.Strict
        });
    }
}