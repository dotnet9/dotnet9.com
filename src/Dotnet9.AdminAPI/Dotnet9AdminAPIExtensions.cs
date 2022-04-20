using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.AdminAPI;

public static class Dotnet9AdminAPIExtensions
{
    public static void AddAdminAPI<T>(this WebApplicationBuilder app) where T : DbContext
    {

    }
}