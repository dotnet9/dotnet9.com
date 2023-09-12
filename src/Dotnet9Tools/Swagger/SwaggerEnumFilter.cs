using System.ComponentModel;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dotnet9Tools.Swagger;

public class SwaggerEnumFilter : IDocumentFilter
{
    /// <summary>
    ///     实现IDocumentFilter接口的Apply函数
    /// </summary>
    /// <param name="swaggerDoc"></param>
    /// <param name="context"></param>
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        Dictionary<string, Type> dict = GetAllEnum();

        foreach (KeyValuePair<string, OpenApiSchema> item in swaggerDoc.Components.Schemas)
        {
            OpenApiSchema? property = item.Value;
            string? typeName = item.Key;
            if (property.Enum != null && property.Enum.Count > 0)
            {
                Type? itemType;
                if (dict.ContainsKey(typeName))
                {
                    itemType = dict[typeName];
                }
                else
                {
                    itemType = null;
                }

                List<OpenApiInteger> list = new List<OpenApiInteger>();
                foreach (IOpenApiAny? val in property.Enum)
                {
                    list.Add((OpenApiInteger)val);
                }

                property.Description += DescribeEnum(itemType!, list);
            }
        }
    }

    private static Dictionary<string, Type> GetAllEnum()
    {
        AssemblyName[]? assList = Assembly.GetEntryAssembly()?.GetReferencedAssemblies();
        Dictionary<string, Type> dict = new Dictionary<string, Type>();
        List<Assembly> list = new List<Assembly>();
        list.Add(Assembly.GetEntryAssembly()!);
        foreach (AssemblyName name in assList!)
        {
            Assembly ass = Assembly.Load(name); //枚举所在的命名空间的xml文件名，我的枚举都放在Model层里（类库）
            list.Add(ass);
        }

        foreach (Assembly assembly in list)
        {
            Type[] types = assembly.GetTypes();
            foreach (Type item in types)
            {
                if (item.IsEnum)
                {
                    dict.TryAdd(item.Name, item);
                }
            }
        }

        return dict;
    }

    private static string DescribeEnum(Type type, List<OpenApiInteger> enums)
    {
        List<string> enumDescriptions = new List<string>();
        foreach (OpenApiInteger item in enums)
        {
            if (type == null)
            {
                continue;
            }

            object value = Enum.Parse(type, item.Value.ToString());
            string desc = GetDescription(type, value);

            if (string.IsNullOrEmpty(desc))
            {
                enumDescriptions.Add($"{item.Value.ToString()}：{Enum.GetName(type, value)}；");
            }
            else
            {
                enumDescriptions.Add($"{item.Value.ToString()}：{Enum.GetName(type, value)}，{desc}；");
            }
        }

        return $"<br><div>{Environment.NewLine}{string.Join("<br/>" + Environment.NewLine, enumDescriptions)}</div>";
    }

    private static string GetDescription(Type t, object value)
    {
        foreach (MemberInfo mInfo in t.GetMembers())
        {
            if (mInfo.Name == t.GetEnumName(value))
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(mInfo))
                {
                    if (attr.GetType() == typeof(DescriptionAttribute))
                    {
                        return ((DescriptionAttribute)attr).Description;
                    }
                }
            }
        }

        return string.Empty;
    }
}