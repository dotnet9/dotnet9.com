namespace Dotnet9.WebAPI.Infrastructure.Tags;

internal class TagRepository : ITagRepository
{
    private readonly Dotnet9DbContext _dbContext;

    public TagRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> DeleteAsync(Guid[] ids)
    {
        var logs = await _dbContext.Tags!.Where(cat => ids.Contains(cat.Id)).ToListAsync();
        _dbContext.RemoveRange(logs);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<Tag?> FindByIdAsync(Guid id)
    {
        return await _dbContext.Tags!.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Tag?> FindByNameAsync(string name)
    {
        return await _dbContext.Tags!.FirstOrDefaultAsync(x => x.Name == name);
    }


    public async Task<(Tag[]? Tags, long Count)> GetListAsync(GetTagListRequest request)
    {
        var query = _dbContext.Tags!.AsQueryable();
        if (!request.Keywords.IsNullOrWhiteSpace())
        {
            query = query.Where(log => EF.Functions.Like(log.Name!, $"%{request.Keywords}%"));
        }

        var dataFromDb = query.Skip((request.Current - 1) * request.PageSize).Take(request.PageSize);
        return (await dataFromDb.ToArrayAsync(), await query.LongCountAsync());
    }
}