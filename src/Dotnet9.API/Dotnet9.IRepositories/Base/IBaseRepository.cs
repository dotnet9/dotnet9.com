using System.Linq.Expressions;

namespace Dotnet9.IRepositories.Base;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

    Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false,
        CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

    Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = false,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false,
        CancellationToken cancellationToken = default);

    Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = false,
        CancellationToken cancellationToken = default);

    Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting,
        CancellationToken cancellationToken = default);

    Task<long> GetCountAsync(CancellationToken cancellationToken = default);

    Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);
}