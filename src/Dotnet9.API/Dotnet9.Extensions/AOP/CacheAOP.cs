using Castle.DynamicProxy;
using Dotnet9.Common.MemoryCache;

namespace Dotnet9.Extensions.AOP;

public class CacheAOP : IInterceptor
{
    private ICachingProvider _cache;
    public CacheAOP(ICachingProvider cache)
    {
        _cache = cache;
    }
    
    public void Intercept(IInvocation invocation)
    {
        var cacheKey = CustomCacheKey(invocation);
        var cacheValue = _cache.Get(cacheKey);
        if (cacheValue != null)
        {
            invocation.ReturnValue = cacheValue;
            return;
        }

        invocation.Proceed();
        if (!string.IsNullOrWhiteSpace(cacheKey))
        {
            _cache.Set(cacheKey, invocation.ReturnValue);
        }
    }

    private string CustomCacheKey(IInvocation invocation)
    {
        var typeName = invocation.TargetType.Name;
        var methodName = invocation.Method.Name;
        var methodArguments = invocation.Arguments.Select(GetArgumentValue).Take(3).ToList();

        var key = $"{typeName}:{methodName}:";
        key = methodArguments.Aggregate(key, (current, param) => current + $"{param}:");

        return key.TrimEnd(':');
    }

    private string GetArgumentValue(object arg)
    {
        switch (arg)
        {
            case int:
            case long:
            case string:
                return arg.ToString();
            case DateTime time:
                return time.ToString("yyyyMMddHHmmss");
            default:
                return "";
        }
    }

}