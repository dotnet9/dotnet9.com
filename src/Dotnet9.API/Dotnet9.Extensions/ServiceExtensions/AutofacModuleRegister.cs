using System.Reflection;
using Autofac;
using Dotnet9.IRepositories.Base;
using Dotnet9.IServices.Base;
using Dotnet9.Repositories.Base;
using Dotnet9.Services.Base;
using Module = Autofac.Module;

namespace Dotnet9.Extensions.ServiceExtensions;

public class AutofacModuleRegister : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();
        builder.RegisterGeneric(typeof(BaseService<>)).As(typeof(IBaseService<>)).InstancePerDependency();

        var assemblyServices = Assembly.Load("Dotnet9.Services");
        builder.RegisterAssemblyTypes(assemblyServices).AsImplementedInterfaces();

        var assemblyRepository = Assembly.Load("Dotnet9.Repositories");
        builder.RegisterAssemblyTypes(assemblyRepository).AsImplementedInterfaces();
    }
}