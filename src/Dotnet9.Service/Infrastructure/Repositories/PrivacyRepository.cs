namespace Dotnet9.Service.Infrastructure.Repositories;

public class PrivacyRepository : Repository<Dotnet9DbContext, Privacy, Guid>, IPrivacyRepository
{
    public PrivacyRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<Privacy?> GetAsync()
    {
        return await Context.Set<Privacy>().FirstOrDefaultAsync();
    }
}