using System.Linq.Expressions;
using Dotnet9Tools.Helper;

namespace Dotnet9.Repositoies;

public abstract class BaseRepository<T, TId> where T : BaseEntity<TId>, new()
    where TId : struct
{
    protected readonly DbContext Ctx;

    public BaseRepository(DbContext ctx)
    {
        Ctx = ctx;
    }

    public async Task<bool> AddAsync(T entity)
    {
        await Ctx.Set<T>().AddAsync(entity);
        int res = await Ctx.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> AddAsync(IEnumerable<T> entitys)
    {
        await Ctx.Set<T>().AddRangeAsync(entitys);
        int res = await Ctx.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        Ctx.Set<T>().Update(entity);
        int res = await Ctx.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        Ctx.Set<T>().Remove(entity);
        int res = await Ctx.SaveChangesAsync();
        return res > 0;
    }

    public async Task<bool> DeleteAsync(List<T> entity)
    {
        Ctx.Set<T>().RemoveRange(entity);
        int res = await Ctx.SaveChangesAsync();
        return res > 0;
    }

    public async Task<T?> FindByIdAsync(object id)
    {
        T? entity = await Ctx.Set<T>().FindAsync(id);
        return entity;
    }

    public async Task<PageDto<T>> GetListAsync(Expression<Func<T, bool>> expression, int index, int size)
    {
        IQueryable<T> query = Ctx.Set<T>().Where(expression);
        List<T> list = await query.Page(index, size).OrderByDescending(a => a.Id).ToListAsync();
        int count = await query.CountAsync();
        return new PageDto<T>(count, list);
    }
}