namespace Dotnet9.WebAPI.EFCore.Abouts;

public class AboutRepository : IAboutRepository
{
    private readonly Dotnet9DbContext _dbContext;

    public AboutRepository(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<About?> GetAboutAsync()
    {
        return await _dbContext.Abouts.FirstOrDefaultAsync();
    }
}