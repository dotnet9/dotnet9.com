namespace Dotnet9.Service.Infrastructure.Repositories;

public class AboutRepository : Repository<Dotnet9DbContext, About, Guid>, IAboutRepository
{
    public AboutRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<About?> GetAsync()
    {
        return Context.Set<About>()
            .AsSplitQuery()
            .FirstOrDefaultAsync().ConfigureAwait(false)
            .GetAwaiter().GetResult();
    }
}