using Microsoft.AspNetCore.Http;

namespace Dotnet9Tools.Auth;

public static class AuthExtensions
{
    /// <summary>
    ///     当前用户
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static Guid CurrUserId(this HttpContext context)
    {
        string? id = context.User.Claims.Where(a => a.Type == "Id").Select(a => a.Value).FirstOrDefault();
        return Guid.Parse(id!);
    }
}