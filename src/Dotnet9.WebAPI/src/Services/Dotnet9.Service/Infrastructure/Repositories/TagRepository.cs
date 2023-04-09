namespace Dotnet9.Service.Infrastructure.Repositories;

public class TagRepository : Repository<Dotnet9DbContext, Tag, Guid>, ITagRepository
{
    public TagRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<Tag?> FindByIdAsync(Guid id)
    {
        return await Context.Tags!.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Tag?> FindByNameAsync(string name)
    {
        return await Context.Tags!.FirstOrDefaultAsync(x => x.Name == name);
    }
}