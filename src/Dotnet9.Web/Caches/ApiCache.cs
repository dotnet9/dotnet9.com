using Dotnet9.Web.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Dotnet9.Web.Caches;

public class ApiCache : ActionFilterAttribute
{
    public int CacheMinutes = 5;
    public bool SignHeader;

    public ApiCache(bool signHeader = false, int cacheMinutes = 5)
    {
        SignHeader = signHeader;
        CacheMinutes = cacheMinutes;
    }


    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var cacheKey = GetKey(filterContext.HttpContext.Request);
        var data = CacheHelper.Cache!.GetAsync<string>(cacheKey).Result;
        if (string.IsNullOrWhiteSpace(data)) return;

        var content = new ContentResult
        {
            Content = data,
            ContentType = "application/json; charset=utf-8",
            StatusCode = 200
        };
        filterContext.HttpContext.Response.Headers.Add("ContentType", "application/json; charset=utf-8");
        filterContext.HttpContext.Response.Headers.Add("CacheData", "Redis");
        filterContext.Result = content;
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        base.OnActionExecuted(filterContext);
    }

    public override void OnResultExecuting(ResultExecutingContext filterContext)
    {
        base.OnResultExecuting(filterContext);
    }

    public override void OnResultExecuted(ResultExecutedContext filterContext)
    {
        if (filterContext.HttpContext.Response.Headers.ContainsKey("CacheData")) return;
        var cacheKey = GetKey(filterContext.HttpContext.Request);
        var data = JsonConvert.SerializeObject(filterContext.Result as ContentResult);
        var disData = JsonConvert.DeserializeObject<Dictionary<string, object>>(data);
        if (disData != null && disData.ContainsKey("data")) CacheMinutes = 1;

        CacheHelper.Cache?.ReplaceAsync(cacheKey, data, TimeSpan.FromMinutes(CacheMinutes));
    }

    private string GetKey(HttpRequest request)
    {
        var keyContent = request.Host.Value + request.Path.Value + request.QueryString.Value + request.Method +
                         request.ContentType + request.ContentLength;
        try
        {
            if (request.Method.ToUpper() != "DELETE" && request.Method.ToUpper() != "GET" && request.Form.Count > 0)
                foreach (var item in request.Form)
                    keyContent += $"{item.Key}={item.Value.ToString()}";
        }
        catch (Exception e)
        {
        }

        if (SignHeader)
        {
            var hs = request.Headers.Where(a => !new[] {"Postman-Token", "User-Agent"}.Contains(a.Key))
                .ToDictionary(a => a);
            foreach (var item in hs) keyContent += $"{item.Key}={item.Value.ToString()}";
        }

        //md5加密
        return CryptographyHelper.Md5Hash(keyContent);
    }
}