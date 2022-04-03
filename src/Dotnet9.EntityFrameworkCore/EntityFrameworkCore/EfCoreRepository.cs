using System.Linq.Expressions;
using Dotnet9.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.EntityFrameworkCore;

public class EfCoreRepository<TEntity>
    where TEntity : EntityBase
{
    protected readonly Dotnet9DbContext DbContext;

    public EfCoreRepository(Dotnet9DbContext dbContext)
    {
        DbContext = dbContext;
    }

    public async Task<Dotnet9DbContext> GetDbContextAsync()
    {
        return await Task.FromResult(DbContext);
    }

    public async Task<int> GetMaxIdAsync()
    {
        if (await DbContext.Set<TEntity>().AnyAsync() == false) return 0;

        return await DbContext.Set<TEntity>().MaxAsync(x => x.Id);
    }

    public async Task<int> InsertAsync(TEntity t)
    {
        await DbContext.Set<TEntity>().AddAsync(t);
        return await DbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(TEntity t)
    {
        DbContext.Set<TEntity>().Update(t);
        return await DbContext.SaveChangesAsync();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> whereLambda,
        params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = DbContext.Set<TEntity>();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.AsNoTracking().FirstOrDefaultAsync(whereLambda);
    }

    public async Task<List<TEntity>> SelectAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = DbContext.Set<TEntity>();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.ToListAsync();
    }

    public async Task<IQueryable<TEntity>> GetQueryableAsync()
    {
        return await Task.FromResult(DbContext.Set<TEntity>().AsQueryable());
    }
}