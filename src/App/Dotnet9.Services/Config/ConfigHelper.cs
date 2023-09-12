using System.Linq.Expressions;
using System.Reflection;

namespace Dotnet9.Services.Config;

public static class ConfigHelper
{
    public static Dictionary<string, string> GetKv<T>(this T model) where T : class, new()
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        string modelName = typeof(T).Name;
        Dictionary<string, string> kv = new Dictionary<string, string>();
        foreach (PropertyInfo item in properties)
        {
            object? v = item.GetValue(model, null);
            string name = item.Name;
            kv.Add($"{modelName}:{name}", v == null ? "" : Convert.ToString(v)!);
        }

        return kv;
    }

    /// <summary>
    ///     获取所有的key
    /// </summary>
    /// <param name="model"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IEnumerable<string> GetAllKeys<T>(T model) where T : class, new()
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        string modelName = typeof(T).Name;
        Dictionary<string, string> kv = new Dictionary<string, string>();
        foreach (PropertyInfo item in properties)
        {
            string name = item.Name;
            yield return $"{modelName}:{name}";
        }
    }

    /// <summary>
    ///     用实体类拼接Key:属性名称
    /// </summary>
    /// <param name="exp"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string GetK<T>(Expression<Func<T, object>> exp) where T : class, new()
    {
        if (exp is not LambdaExpression expression)
        {
            throw new Exception("没有找到key");
        }

        return expression.Body switch
        {
            MemberExpression member => $"{typeof(T).Name}:{member.Member.Name}",
            _ => throw new Exception("没有实现当前类型")
        };
    }
}