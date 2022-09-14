namespace Dotnet9.ASPNETCore.Filters;

public class ResultWrapperFilter : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        var actionWrapper = controllerActionDescriptor?.MethodInfo
            .GetCustomAttributes(typeof(NoWrapperAttribute), false).FirstOrDefault();
        var controllerWrapper = controllerActionDescriptor?.ControllerTypeInfo
            .GetCustomAttributes(typeof(NoWrapperAttribute), false).FirstOrDefault();
        if (actionWrapper != null || controllerWrapper != null)
        {
            return;
        }

        if (context.Result is not ObjectResult objectResult)
        {
            return;
        }

        if (objectResult is BadRequestObjectResult)
        {
            context.Result =
                new ObjectResult(ResponseResult<object>.Result(HttpStatusCode.BadRequest, "", objectResult.Value));
        }
        else if (objectResult.Value == null)
        {
            context.Result = new ObjectResult(ResponseResult<object>.Error(HttpStatusCode.NotFound, "未找到资源"));
        }
        else
        {
            if (objectResult.DeclaredType?.IsGenericType == true &&
                objectResult.DeclaredType?.GetGenericTypeDefinition() ==
                typeof(ResponseResult<>))
            {
                return;
            }

            context.Result = new ObjectResult(ResponseResult<object>.Success(objectResult.Value));
        }
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
public class NoWrapperAttribute : Attribute
{
}