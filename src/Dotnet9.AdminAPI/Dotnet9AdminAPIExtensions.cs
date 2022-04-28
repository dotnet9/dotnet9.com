using Dotnet9.AdminAPI.AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnet9.AdminAPI;

public static class Dotnet9AdminAPIExtensions
{
    public static void AddAdminAPI<T>(this WebApplicationBuilder app) where T : DbContext
    {
        app.Services.AddAutoMapperSetup();
        app.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
    }
}