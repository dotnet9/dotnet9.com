using System.Data;
using Dotnet9.AnyDBConfigProvider;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Configuration;

public static class DbConfigurationProviderExtensions
{
    public static IConfigurationBuilder AddDbConfiguration(this IConfigurationBuilder builder, DbConfigOptions options)
    {
        return builder.Add(new DbConfigurationSource(options));
    }

    public static IConfigurationBuilder AddDbConfiguration(this IConfigurationBuilder builder,
        Func<IDbConnection> createDbConnection, string tableName = "T_Configs", bool reloadOnChange = false,
        TimeSpan? reloadInterval = null)
    {
        return AddDbConfiguration(builder,
            new DbConfigOptions
            {
                CreateDbConnection = createDbConnection, TableName = tableName, ReloadOnChange = reloadOnChange,
                ReloadInterval = reloadInterval
            });
    }
}