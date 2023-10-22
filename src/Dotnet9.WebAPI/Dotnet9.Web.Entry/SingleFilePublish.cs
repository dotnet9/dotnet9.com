using Furion;
using System.Reflection;

namespace Dotnet9.Web.Entry;

public class SingleFilePublish : ISingleFilePublish
{
    public Assembly[] IncludeAssemblies()
    {
        return Array.Empty<Assembly>();
    }

    public string[] IncludeAssemblyNames()
    {
        return new[]
        {
            "Dotnet9.Application",
            "Dotnet9.Core",
            "Dotnet9.Web.Core"
        };
    }
}