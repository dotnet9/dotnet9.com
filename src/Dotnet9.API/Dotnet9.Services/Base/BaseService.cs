using Dotnet9.IRepositories.Base;
using Dotnet9.IServices.Base;
using System.Linq.Expressions;

namespace Dotnet9.Services.Base;

public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
{
    private readonly IBaseRepository<TEntity> _repository;

    public BaseService(IBaseRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        return await _repository.InsertAsync(entity, autoSave, cancellationToken);
    }

    public async Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        await _repository.InsertManyAsync(entities, autoSave, cancellationToken);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        return await _repository.UpdateAsync(entity, autoSave, cancellationToken);
    }

    public async Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        await _repository.UpdateManyAsync(entities, autoSave, cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync(entity, autoSave, cancellationToken);
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        await _repository.DeleteAsync(predicate, autoSave, cancellationToken);
    }

    public async Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        await _repository.DeleteManyAsync(entities, autoSave, cancellationToken);
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _repository.FindAsync(predicate, cancellationToken);
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _repository.GetAsync(predicate, cancellationToken);
    }

    public async Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.GetListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _repository.GetListAsync(predicate, cancellationToken);
    }

    public async Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting,
        CancellationToken cancellationToken = default)
    {
        return await _repository.GetPagedListAsync(skipCount, maxResultCount, sorting, cancellationToken);
    }

    public async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
    {
        return await _repository.GetCountAsync(cancellationToken);
    }

    public async Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _repository.GetCountAsync(predicate, cancellationToken);
    }
}