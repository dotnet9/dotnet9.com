namespace Dotnet9.WebAPI.Domain.Donations;

public interface IDonationRepository
{
    Task<Donation?> GetAsync();
}