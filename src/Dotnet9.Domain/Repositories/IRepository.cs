using System.Linq.Expressions;

namespace Dotnet9.Domain.Repositories;

public interface IRepository<TEntity>
{
    Task<int> GetMaxIdAsync();
    Task<List<TEntity>> SelectAsync(params Expression<Func<TEntity, object>>[] includes);
    Task<IQueryable<TEntity>> GetQueryableAsync();
}