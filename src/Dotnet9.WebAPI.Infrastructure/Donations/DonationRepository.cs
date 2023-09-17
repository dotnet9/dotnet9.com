namespace Dotnet9.WebAPI.Infrastructure.Donations;

public class DonationRepository : IDonationRepository
{
    private readonly Dotnet9DbContext _dbContext;

    public DonationRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Donation?> GetAsync()
    {
        return await _dbContext.Donations!.FirstOrDefaultAsync();
    }
}