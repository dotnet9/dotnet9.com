namespace Dotnet9.Service.Infrastructure.Repositories;

public class TagRepository : Repository<Dotnet9DbContext, Tag, Guid>, ITagRepository
{
    private readonly IMultilevelCacheClient _multilevelCacheClient;

    public TagRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork, IMultilevelCacheClient multilevelCacheClient)
        : base(context, unitOfWork)
    {
        _multilevelCacheClient = multilevelCacheClient;
    }

    public Task<Tag?> FindByIdAsync(Guid id)
    {
        return Context.Tags!.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Tag?> FindByNameAsync(string name)
    {
        return Context.Tags!.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<TagBrief>?> GetHotTagBriefListAsync()
    {
        async Task<List<TagBrief>?> ReadDataFromDb()
        {
            var randomData = await Context.Tags.Take(10).Select(tag =>
                    new TagBrief(tag.Name, Context.Set<BlogTag>().Count((blogTag => blogTag.TagId == tag.Id))))
                .ToListAsync();

            return randomData;
        }

        TimeSpan? timeSpan = null;
        const string key = $"{nameof(TagRepository)}_{nameof(GetHotTagBriefListAsync)}";

        var data = await _multilevelCacheClient.GetOrSetAsync(key, async () =>
        {
            var dataFromDb = await ReadDataFromDb();

            if (dataFromDb?.Any() == true)
            {
                timeSpan = TimeSpan.FromSeconds(30);
                return new CacheEntry<List<TagBrief>>(dataFromDb, TimeSpan.FromDays(3))
                {
                    SlidingExpiration = TimeSpan.FromMinutes(5)
                };
            }

            timeSpan = TimeSpan.FromSeconds(5);
            return new CacheEntry<List<TagBrief>>(null);
        }, options =>
            options.AbsoluteExpirationRelativeToNow = timeSpan);

        return data;
    }
}