using Autofac;
using System.Reflection;

namespace Dotnet9.Extensions.ServiceExtensions;

public class AutofacModuleRegister : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var basePath = AppContext.BaseDirectory;
        var servicesDllFile = Path.Combine(basePath, "Dotnet9.Services.dll");
        var repositoryDllFile = Path.Combine(basePath, "Dotnet9.Repositories.dll");
        if (!(File.Exists(servicesDllFile) && File.Exists(repositoryDllFile)))
        {
            throw new Exception("Repositories.dd 和 Services.dll丢失。");
        }


        var assemblyServices = Assembly.LoadFrom(servicesDllFile);
        builder.RegisterAssemblyTypes(assemblyServices).AsImplementedInterfaces();

        var assemblyRepository = Assembly.LoadFrom(repositoryDllFile);
        builder.RegisterAssemblyTypes(assemblyRepository).AsImplementedInterfaces();
    }
}