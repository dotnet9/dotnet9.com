namespace Dotnet9.Web.HttpContext;

public static class HttpContextExtensions
{
    public static string GetClientIP(this IHttpContextAccessor httpContextAccessor)
    {
        var request = httpContextAccessor.HttpContext!.Request;
        const string readIpKey = "X-Real-IP";
        if (request.Headers.ContainsKey(readIpKey))
        {
            return request.Headers[readIpKey].ToString();
        }

        const string forwardedForKey = "X-Forwarded-For";
        return request.Headers.ContainsKey(forwardedForKey) ? request.Headers[forwardedForKey].ToString() : "";
    }

    public static bool IsAjax(this HttpRequest req)
    {
        var result = false;

        var requestedWith = "x-requested-with";
        var xreq = req.Headers.ContainsKey(requestedWith);
        if (xreq)
        {
            result = req.Headers[requestedWith] == "XMLHttpRequest";
        }

        return result;
    }
}