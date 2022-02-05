using Autofac;
using Autofac.Extras.DynamicProxy;
using Dotnet9.Common.Helpers;
using Dotnet9.Extensions.AOP;
using Dotnet9.IRepositories.Base;
using Dotnet9.IServices.Base;
using Dotnet9.Repositories.Base;
using Dotnet9.Services.Base;
using System.Reflection;
using Module = Autofac.Module;

namespace Dotnet9.Extensions.ServiceExtensions;

public class AutofacModuleRegister : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        var cacheType = new List<Type>();

        if (Appsettings.App(new string[] { "AppSettings", "MemoryCachingAOP", "Enabled" }).ObjToBool())
        {
            builder.RegisterType<CacheAOP>();
            cacheType.Add(typeof(CacheAOP));
        }

        if (Appsettings.App(new string[] { "AppSettings", "LogAOP", "Enabled" }).ObjToBool())
        {
            builder.RegisterType<LogAOP>();
            cacheType.Add(typeof(LogAOP));
        }

        builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();
        builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>)).InstancePerDependency();

        var assemblyServices = Assembly.Load("Dotnet9.Services");
        builder.RegisterAssemblyTypes(assemblyServices)
            .AsImplementedInterfaces()
            .EnableInterfaceInterceptors()
            .InterceptedBy(cacheType.ToArray());

        var assemblyRepository = Assembly.Load("Dotnet9.Repositories");
        builder.RegisterAssemblyTypes(assemblyRepository).AsImplementedInterfaces();
    }
}