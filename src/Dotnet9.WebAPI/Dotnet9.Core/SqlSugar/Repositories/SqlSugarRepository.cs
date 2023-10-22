namespace SqlSugar;
/// <summary>
/// 泛型仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class SqlSugarRepository<T> : SimpleClient<T>, ISqlSugarRepository<T> where T : class, new()
{
    private readonly ISqlSugarClient _client;

    public SqlSugarRepository(ISqlSugarClient client) : base(client)
    {
        _client = client;
    }
}