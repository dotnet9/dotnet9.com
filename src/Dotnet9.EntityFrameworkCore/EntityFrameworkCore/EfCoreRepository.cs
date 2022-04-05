using System.Linq.Expressions;
using Dotnet9.Domain;
using Dotnet9.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.EntityFrameworkCore;

public class EfCoreRepository<T>
    where T : EntityBase
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
        if (await DbContext.Set<T>().AnyAsync() == false) return 0;

        return await DbContext.Set<T>().MaxAsync(x => x.Id);
    }

    public async Task<int> InsertAsync(T t)
    {
        await DbContext.Set<T>().AddAsync(t);
        return await DbContext.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(T t)
    {
        DbContext.Set<T>().Update(t);
        return await DbContext.SaveChangesAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> whereLambda,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbContext.Set<T>();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.AsNoTracking().FirstOrDefaultAsync(whereLambda);
    }

    public async Task<List<T>> SelectAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbContext.Set<T>();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.ToListAsync();
    }

    public async Task<List<T>> SelectAsync(Expression<Func<T, bool>> whereLambda,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = DbContext.Set<T>();
        query = includes.Aggregate(query, (current, include) => current.Include(include));
        return await query.Where(whereLambda).ToListAsync();
    }

    public async Task<List<T>> SelectAsync<S>(Expression<Func<T, bool>> whereLambda,
        Expression<Func<T, S>> orderByLambda, SortDirectionKind sortDirection,
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T>? query;

        if (sortDirection == SortDirectionKind.Ascending)
            query = DbContext.Set<T>().AsNoTracking().Where(whereLambda)
                .OrderBy(orderByLambda);
        else
            query = DbContext.Set<T>().AsNoTracking().Where(whereLambda)
                .OrderByDescending(orderByLambda);
        foreach (var include in includes) query = query.Include(include);
        return await query.ToListAsync();
    }


    public async Task<Tuple<List<T>, int>> SelectAsync<S>(int pageSize, int pageIndex,
        Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda, SortDirectionKind sortDirection,
        params Expression<Func<T, object>>[] includes)
    {
        var total = await DbContext.Set<T>().Where(whereLambda).CountAsync();
        IQueryable<T>? query = null;

        if (sortDirection == SortDirectionKind.Ascending)
            query = DbContext.Set<T>().Where(whereLambda)
                .OrderBy(orderByLambda);
        else
            query = DbContext.Set<T>().Where(whereLambda)
                .OrderByDescending(orderByLambda);
        foreach (var include in includes) query = query.Include(include);
        var lst = await query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
        return new Tuple<List<T>, int>(lst, total);
    }

    public async Task<IQueryable<T>> GetQueryableAsync()
    {
        return await Task.FromResult(DbContext.Set<T>().AsQueryable());
    }
}