// ReSharper disable once CheckNamespace

namespace Microsoft.Extensions.Configuration;

public class DBConfigOptions
{
    public Func<IDbConnection> CreateDbConnection { get; set; }
    public string TableName { get; set; } = "T_Configs";
    public bool ReloadOnChange { get; set; } = false;
    public TimeSpan? ReloadInterval { get; set; }
}