namespace Dotnet9.Caller;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDotnet9ApiGateways(this IServiceCollection services)
    {
        services.AddAutoRegistrationCaller(typeof(ServiceCollectionExtensions).Assembly);

        return services;
    }
}