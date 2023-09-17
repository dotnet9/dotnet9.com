namespace Dotnet9.ASPNETCore.Filters;

public class CustomValidationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        switch (context.HttpContext.Response.StatusCode)
        {
            case (int)HttpStatusCode.Unauthorized:
            {
                ResponseResult<object> result =
                    ResponseResult<object>.GetError(HttpStatusCode.Unauthorized, "登录已失效，请重新登录");
                context.Result = new ObjectResult(result);
                break;
            }
            case (int)HttpStatusCode.Forbidden:
            {
                ResponseResult<object> result = ResponseResult<object>.GetError(HttpStatusCode.Forbidden, "您无权访问");
                context.Result = new ObjectResult(result);
                break;
            }
            default:
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

                break;
            }
        }
    }
}