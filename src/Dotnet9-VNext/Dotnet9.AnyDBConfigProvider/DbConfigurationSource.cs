using Microsoft.Extensions.Configuration;

namespace Dotnet9.AnyDBConfigProvider;

public class DbConfigurationSource : IConfigurationSource
{
    private readonly DbConfigOptions _options;

    public DbConfigurationSource(DbConfigOptions options)
    {
        _options = options;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new DbConfigurationProvider(_options);
    }
}