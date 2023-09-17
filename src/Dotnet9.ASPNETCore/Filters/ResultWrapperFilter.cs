namespace Dotnet9.ASPNETCore.Filters;

public class ResultWrapperFilter : ActionFilterAttribute
{
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        ControllerActionDescriptor? controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        object? actionWrapper = controllerActionDescriptor?.MethodInfo
            .GetCustomAttributes(typeof(NoWrapperAttribute), false).FirstOrDefault();
        object? controllerWrapper = controllerActionDescriptor?.ControllerTypeInfo
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
            if (!context.ModelState.IsValid)
            {
                List<string> errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

                context.Result =
                    new ObjectResult(ResponseResult<object>.GetError(HttpStatusCode.BadRequest,
                        errors.JoinAsString(",")));
            }
            else
            {
                context.Result =
                    new ObjectResult(ResponseResult<object>.GetResult(false, HttpStatusCode.BadRequest, "",
                        objectResult.Value));
            }
        }
        else if (objectResult.Value == null)
        {
            context.Result = new ObjectResult(ResponseResult<object>.GetError(HttpStatusCode.NotFound, "未找到资源"));
        }
        else
        {
            if (objectResult.DeclaredType?.IsGenericType == true &&
                objectResult.DeclaredType?.GetGenericTypeDefinition() ==
                typeof(ResponseResult<>))
            {
                return;
            }

            context.Result = new ObjectResult(ResponseResult<object>.GetSuccess(objectResult.Value));
        }
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
public class NoWrapperAttribute : Attribute
{
}