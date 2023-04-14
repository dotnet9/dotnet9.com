namespace Dotnet9.Service.Infrastructure;

public static class Dotnet9DbContextSeed
{
    public static Task SeedAsync(Dotnet9DbContext dotnet9DbContext, IServiceProvider serviceProvider)
    {
        return Task.CompletedTask;
    }
}
