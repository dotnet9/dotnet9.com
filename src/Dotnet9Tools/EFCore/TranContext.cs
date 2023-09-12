using Dotnet9Tools.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Dotnet9Tools.EFCore;

/// <summary>
///     DbContext 事务管理
/// </summary>
public class TranContext
{
    private readonly DbContext _context;

    private readonly ILogger _logger;

    private IDbContextTransaction? _tran;

    private int _tranCount;

    public TranContext(DbContext context, ILogger<TranContext> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    ///     开启事务
    /// </summary>
    /// <returns></returns>
    public async Task BeginTranAsync()
    {
        _tran ??= await _context.Database.BeginTransactionAsync();
        _tranCount += 1;
    }

    /// <summary>
    ///     提交事务
    /// </summary>
    /// <returns></returns>
    /// <exception cref="UserException"></exception>
    public async Task CommitTranAsync()
    {
        if (_tran == null)
        {
            throw new UserException("事务未开启");
        }

        _tranCount -= 1;
        if (_tranCount == 0)
        {
            await _tran?.CommitAsync()!;
        }
        else
        {
            _logger.LogInformation("事务计数:" + _tranCount);
        }
    }
}