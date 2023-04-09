namespace Dotnet9.Service.Infrastructure.Repositories;

public class DonationRepository : Repository<Dotnet9DbContext, Donation, Guid>, IDonationRepository
{
    public DonationRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<Donation?> GetAsync()
    {
        return await Context.Donations!.FirstOrDefaultAsync();
    }
}