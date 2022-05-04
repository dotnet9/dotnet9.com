using System.Linq.Expressions;

namespace Dotnet9.Domain.Repositories;

public interface IRepository<T>
{
    Task<int> CountAsync();
    Task<int> GetMaxIdAsync();

    Task<int> InsertAsync(T t);

    Task<int> UpdateAsync(T t);

    Task<T?> GetAsync(Expression<Func<T, bool>> whereLambda,
        params Expression<Func<T, object>>[] includes);

    Task<List<T>> SelectAsync(params Expression<Func<T, object>>[] includes);

    Task<List<T>> SelectAsync(Expression<Func<T, bool>> whereLambda,
        params Expression<Func<T, object>>[] includes);

    Task<List<T>> SelectAsync<S>(Expression<Func<T, bool>> whereLambda, Expression<Func<T, S>> orderByLambda,
        SortDirectionKind sortDirection, params Expression<Func<T, object>>[] includes);

    Task<Tuple<List<T>, int>> SelectAsync<S>(int pageSize, int pageIndex, Expression<Func<T, bool>> whereLambda,
        Expression<Func<T, S>> orderByLambda, SortDirectionKind sortDirection,
        params Expression<Func<T, object>>[] includes);

    Task<IQueryable<T>> GetQueryableAsync();
}