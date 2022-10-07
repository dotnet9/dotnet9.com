namespace Dotnet9.ASPNETCore.Filters;

public class UnitOfWorkFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var uowAttr = GetUoWAttr(context.ActionDescriptor);
        if (uowAttr == null)
        {
            await next();
            return;
        }

        using TransactionScope txScope = new(TransactionScopeAsyncFlowOption.Enabled);
        var dbCtxs = new List<DbContext>();
        foreach (var dbCtxType in uowAttr.DbContextTypes)
        {
            //用HttpContext的RequestServices
            //确保获取的是和请求相关的Scope实例
            var sp = context.HttpContext.RequestServices;
            var dbCtx = (DbContext)sp.GetRequiredService(dbCtxType);
            dbCtxs.Add(dbCtx);
        }

        var result = await next();
        if (result.Exception == null)
        {
            foreach (var dbCtx in dbCtxs)
            {
                await dbCtx.SaveChangesAsync();
            }

            txScope.Complete();
        }
    }

    private static UnitOfWorkAttribute? GetUoWAttr(ActionDescriptor actionDesc)
    {
        var caDesc = actionDesc as ControllerActionDescriptor;
        if (caDesc == null)
        {
            return null;
        }

        //try to get UnitOfWorkAttribute from controller,
        //if there is no UnitOfWorkAttribute on controller, 
        //try to get UnitOfWorkAttribute from action
        var uowAttr = caDesc.ControllerTypeInfo
            .GetCustomAttribute<UnitOfWorkAttribute>();
        if (uowAttr != null)
        {
            return uowAttr;
        }

        return caDesc.MethodInfo
            .GetCustomAttribute<UnitOfWorkAttribute>();
    }
}