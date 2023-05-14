namespace Dotnet9.Service.Services;

public interface ISeedService: IScopedDependency
{
    Task MigrateAsync();
}