namespace Dotnet9.WebAPI.Infrastructure.Privacies;

public class PrivacyRepository : IPrivacyRepository
{
    private readonly Dotnet9DbContext _dbContext;

    public PrivacyRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Privacy?> GetAsync()
    {
        return await _dbContext.Privacies.FirstOrDefaultAsync();
    }
}