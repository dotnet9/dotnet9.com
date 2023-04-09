namespace Dotnet9.Service.Services;

public interface ISeedService
{
    Task MigrateAsync();
}