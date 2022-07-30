namespace Dotnet9.Commons;

public interface IModuleInitializer
{
    void Initialize(IServiceCollection services);
}