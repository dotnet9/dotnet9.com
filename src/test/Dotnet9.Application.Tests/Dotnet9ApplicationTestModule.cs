using Volo.Abp.Modularity;

namespace Dotnet9
{
    [DependsOn(
        typeof(Dotnet9ApplicationModule),
        typeof(Dotnet9DomainTestModule)
        )]
    public class Dotnet9ApplicationTestModule : AbpModule
    {

    }
}