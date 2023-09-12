namespace Dotnet9.Repositoies.Blogs;

public class PostCatesRepository : BaseRepository<PostCates, int>
{
    public PostCatesRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<List<PostCates>> GetCatesAsync(List<int> ids)
    {
        return await Ctx.Set<PostCates>().Where(a => ids.Contains(a.Id)).ToListAsync();
    }

    public async Task PostSetCateRelation(Posts post, List<PostCates> list)
    {
        await Ctx.Set<PostCateRelation>().Where(a => a.Post == post).ExecuteDeleteAsync();
        foreach (PostCates item in list)
        {
            await Ctx.Set<PostCateRelation>()
                .AddAsync(new PostCateRelation
                {
                    Post = post, PostCate = item
                });
        }

        await Ctx.SaveChangesAsync();
    }
}