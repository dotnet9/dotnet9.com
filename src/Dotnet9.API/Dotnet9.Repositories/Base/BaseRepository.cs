using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Dotnet9.EntityFramework;
using Dotnet9.IRepositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.Repositories.Base;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
{
    private readonly Dotnet9Context _context;

    public BaseRepository(Dotnet9Context context)
    {
        _context = context;
    }

    public async Task<TEntity> InsertAsync(TEntity entity, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        var savedEntity = (await _context.Set<TEntity>().AddAsync(entity, cancellationToken)).Entity;

        if (autoSave) await _context.SaveChangesAsync(cancellationToken);

        return savedEntity;
    }

    public async Task InsertManyAsync(IEnumerable<TEntity> entities, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        var entityArray = entities.ToArray();

        await _context.Set<TEntity>().AddRangeAsync(entityArray, cancellationToken);

        if (autoSave) await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        _context.Attach(entity);

        var updateEntity = _context.Update(entity).Entity;

        if (autoSave) await _context.SaveChangesAsync(cancellationToken);

        return updateEntity;
    }

    public async Task UpdateManyAsync(IEnumerable<TEntity> entities, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().UpdateRange(entities);

        if (autoSave) await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
    {
        _context.Set<TEntity>().Remove(entity);

        if (autoSave) await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Expression<Func<TEntity, bool>> predicate, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        var dbSet = _context.Set<TEntity>();

        var entities = await dbSet
            .Where(predicate)
            .ToListAsync(cancellationToken);

        await DeleteManyAsync(entities, autoSave, cancellationToken);

        if (autoSave) await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteManyAsync(IEnumerable<TEntity> entities, bool autoSave = false,
        CancellationToken cancellationToken = default)
    {
        _context.RemoveRange(entities);

        if (autoSave) await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Where(predicate).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetPagedListAsync(int skipCount, int maxResultCount, string sorting,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().OrderBy(sorting).Skip(skipCount).Take(maxResultCount)
            .ToListAsync(cancellationToken);
    }

    public async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>().LongCountAsync(cancellationToken);
    }

    public async Task<long> GetCountAsync(Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _context.Set<TEntity>().Where(predicate).LongCountAsync(cancellationToken);
    }

    protected Dotnet9Context DbContext()
    {
        return _context;
    }
}