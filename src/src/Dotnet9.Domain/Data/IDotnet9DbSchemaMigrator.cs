using System.Threading.Tasks;

namespace Dotnet9.Data
{
    public interface IDotnet9DbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
