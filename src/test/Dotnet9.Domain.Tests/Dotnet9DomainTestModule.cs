using Dotnet9.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Dotnet9
{
    [DependsOn(
        typeof(Dotnet9EntityFrameworkCoreTestModule)
        )]
    public class Dotnet9DomainTestModule : AbpModule
    {

    }
}