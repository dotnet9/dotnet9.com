using Furion.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Dotnet9.Application.Menu;
using Furion;
using Furion.DataEncryption;

namespace Dotnet9.Web.Core;

public class JwtHandler : AppAuthorizeHandler
{
    /// <summary>
    /// 这里写您的授权判断逻辑，授权通过返回 true，否则返回 false
    /// </summary>
    /// <param name="context"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public override Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        string code = httpContext.Request.Path.Value!.Replace("/api/", "").Replace("/", ":");
        var sysMenuService = App.GetService<SysMenuService>(httpContext.RequestServices);
        //判断访问权限
        return sysMenuService.CheckPermission(code);
    }

    /// <summary>
    /// 自动刷新token
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task HandleAsync(AuthorizationHandlerContext context)
    {
        // 自动刷新 token
        if (JWTEncryption.AutoRefreshToken(context, context.GetCurrentHttpContext()))
        {
            await AuthorizeHandleAsync(context);
        }
        else
        {
            context.Fail();    // 授权失败
            DefaultHttpContext currentHttpContext = context.GetCurrentHttpContext();
            currentHttpContext?.SignoutToSwagger();
        }
    }
}
