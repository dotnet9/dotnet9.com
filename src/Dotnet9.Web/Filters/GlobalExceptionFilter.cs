using System.Net;
using Dotnet9.Web.HttpContext;
using Dotnet9.Web.ViewModels.Accounts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Dotnet9.Web.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not UserException exception) return;

        if (context.HttpContext.Request.IsAjax())
        {
            context.Result = new JsonResult(new {exception.Message});
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        }
        else
        {
            context.HttpContext.Items["ErrorMsg"] = exception.Message;
            context.ExceptionHandled = true;
            context.HttpContext.Response.Redirect("/404.html");
        }
    }
}