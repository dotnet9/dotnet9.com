using System.Linq.Expressions;

namespace Dotnet9.Domain.Repositories;

public interface IRepository<TEntity>
{
    Task<int> GetMaxIdAsync();

    Task<int> InsertAsync(TEntity t);

    Task<int> UpdateAsync(TEntity t);

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> whereLambda,
        params Expression<Func<TEntity, object>>[] includes);

    Task<List<TEntity>> SelectAsync(params Expression<Func<TEntity, object>>[] includes);

    Task<IQueryable<TEntity>> GetQueryableAsync();
}