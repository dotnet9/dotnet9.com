using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Dotnet9.Data;
using Volo.Abp.DependencyInjection;

namespace Dotnet9.EntityFrameworkCore
{
    public class EntityFrameworkCoreDotnet9DbSchemaMigrator
        : IDotnet9DbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreDotnet9DbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the Dotnet9DbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<Dotnet9DbContext>()
                .Database
                .MigrateAsync();
        }
    }
}
