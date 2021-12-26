using Dotnet9.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Dotnet9.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(Dotnet9EntityFrameworkCoreModule),
        typeof(Dotnet9ApplicationContractsModule)
        )]
    public class Dotnet9DbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
