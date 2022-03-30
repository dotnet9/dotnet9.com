using System.Linq.Expressions;
using Dotnet9.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.EntityFrameworkCore;

public class EfCoreRepository<TEntity>
    where TEntity : EntityBase
{
    private readonly Dotnet9DbContext _dbContext;

    public EfCoreRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Dotnet9DbContext> GetDbContextAsync()
    {
        return await Task.FromResult(_dbContext);
    }

    public async Task<int> GetMaxIdAsync()
    {
        if (await _dbContext.Set<TEntity>().AnyAsync() == false) return 0;

        return await _dbContext.Set<TEntity>().MaxAsync(x => x.Id);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> whereLambda,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.AsNoTracking().FirstOrDefaultAsync(whereLambda);
    }

    public async Task<List<TEntity>> SelectAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.ToListAsync();
    }

    public async Task<IQueryable<TEntity>> GetQueryableAsync()
    {
        return await Task.FromResult(_dbContext.Set<TEntity>().AsQueryable());
    }
}