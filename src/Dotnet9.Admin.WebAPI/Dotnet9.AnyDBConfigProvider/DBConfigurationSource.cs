namespace Microsoft.Extensions.Configuration;

internal class DBConfigurationSource : IConfigurationSource
{
    private readonly DBConfigOptions options;

    public DBConfigurationSource(DBConfigOptions options)
    {
        this.options = options;
    }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new DBConfigurationProvider(options);
    }
}