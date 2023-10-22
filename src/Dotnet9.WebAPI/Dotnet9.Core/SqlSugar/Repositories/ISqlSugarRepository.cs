namespace SqlSugar;
/// <summary>
/// 泛型仓储
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISqlSugarRepository<T> : ISimpleClient<T> where T : class, new()
{

}