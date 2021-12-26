using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Dotnet9.Data
{
    /* This is used if database provider does't define
     * IDotnet9DbSchemaMigrator implementation.
     */
    public class NullDotnet9DbSchemaMigrator : IDotnet9DbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}