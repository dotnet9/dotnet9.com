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
        List<Tag> tags = await _dbContext.Tags!.Where(cat => ids.Contains(cat.Id)).ToListAsync();
        _dbContext.RemoveRange(tags);
        await _dbContext.Set<BlogPostTag>().Where(tag => ids.Contains(tag.TagId)).ExecuteDeleteAsync();

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


    public async Task<(TagDto[]? Tags, long Count)> GetListAsync(GetTagListRequest request)
    {
        IQueryable<Tag> query = _dbContext.Tags!.AsQueryable();
        if (!request.Keywords.IsNullOrWhiteSpace())
        {
            query = query.Where(log => EF.Functions.Like(log.Name.ToLower()!, $"%{request.Keywords!.ToLower()}%"));
        }

        TagDto[] dataFromDb = await query.OrderByDescending(tag => tag.CreationTime)
            .Skip((request.Current - 1) * request.PageSize).Take(request.PageSize).Select(tag =>
                new TagDto(tag.Id, tag.Name, _dbContext.Set<BlogPostTag>()
                    .Count(blogPostTag => blogPostTag.TagId == tag.Id), tag.CreationTime))
            .ToArrayAsync();
        return (dataFromDb, await query.LongCountAsync());
    }
}