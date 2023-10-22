namespace SqlSugar;

/// <summary>
/// 数据库事务（工作单元保证数据的一致性）
/// </summary>
public class SqlSugarUnitOfWork : IUnitOfWork
{
    private readonly ISqlSugarClient _client;

    public SqlSugarUnitOfWork(ISqlSugarClient client)
    {
        _client = client;
    }

    /// <summary>
    /// 开启事务
    /// </summary>
    /// <param name="context"></param>
    /// <param name="unitOfWork"></param>
    public void BeginTransaction(FilterContext context, UnitOfWorkAttribute unitOfWork)
    {
        _client.Ado.BeginTran();
    }

    /// <summary>
    /// 提交事务
    /// </summary>
    /// <param name="resultContext"></param>
    /// <param name="unitOfWork"></param>
    public void CommitTransaction(FilterContext resultContext, UnitOfWorkAttribute unitOfWork)
    {
        _client.Ado.CommitTran();
    }

    /// <summary>
    /// 回滚
    /// </summary>
    /// <param name="resultContext"></param>
    /// <param name="unitOfWork"></param>
    public void RollbackTransaction(FilterContext resultContext, UnitOfWorkAttribute unitOfWork)
    {
        _client.Ado.RollbackTran();
    }

    /// <summary>
    /// 释放连接对象
    /// </summary>
    /// <param name="context"></param>
    /// <param name="resultContext"></param>
    public void OnCompleted(FilterContext context, FilterContext resultContext)
    {
        _client.Dispose();
    }
}