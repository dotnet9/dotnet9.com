namespace Dotnet9.Service.Infrastructure.Repositories;

public class DonationRepository : Repository<Dotnet9DbContext, Donation, Guid>, IDonationRepository
{
    public DonationRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public Task<Donation?> GetAsync()
    {
        return Context.Set<Donation>().FirstOrDefaultAsync();
    }
}