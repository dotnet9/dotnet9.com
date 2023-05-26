namespace Dotnet9.Service.Infrastructure.Repositories;

public class TagRepository : Repository<Dotnet9DbContext, Tag, Guid>, ITagRepository
{
    public TagRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork)
        : base(context, unitOfWork)
    {
    }

    public Task<Tag?> FindByIdAsync(Guid id)
    {
        return Context.Tags!.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<Tag?> FindByNameAsync(string name)
    {
        return Context.Tags!.FirstOrDefaultAsync(x => x.Name == name);
    }

    public async Task<List<TagBrief>?> GetTagBriefListAsync()
    {
        var dataFromDb = await Context.Tags.Select(tag =>
                new TagBrief(tag.Name, Context.Set<BlogTag>().Count((blogTag => blogTag.TagId == tag.Id))))
            .ToListAsync();

        return dataFromDb;
    }
}