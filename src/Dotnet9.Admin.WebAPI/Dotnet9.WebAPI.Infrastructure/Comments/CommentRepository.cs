namespace Dotnet9.WebAPI.Infrastructure.Comments;

internal class CommentRepository : ICommentRepository
{
    private readonly Dotnet9DbContext _dbContext;

    public CommentRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> DeleteAsync(Guid[] ids)
    {
        var datas = await _dbContext.Comments!.Where(c => ids.Contains(c.Id)).ToListAsync();
        _dbContext.RemoveRange(datas);
        return await _dbContext.SaveChangesAsync();
    }

    public async Task<Comment?> FindByIdAsync(Guid id)
    {
        return await _dbContext.Comments!.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<(Comment[]? Comments, long Count)> GetListAsync(GetCommentListRequest request)
    {
        IQueryable<Comment> query = _dbContext.Comments!.AsQueryable();

        var categoriesFromDb = query.Where(x => x.ParentId == request.ParentId && x.Url == request.Url)
            .OrderBy(x => x.CreationTime)
            .Skip((request.Current - 1) * request.PageSize).Take(request.PageSize);
        return (await categoriesFromDb.ToArrayAsync(), await query.LongCountAsync());
    }
}