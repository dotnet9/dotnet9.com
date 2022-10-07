namespace Dotnet9.Application.Contracts.Donations;

public interface IDonationAppService
{
    Task<DonationDto?> GetAsync();
    Task<bool> UpdateAsync(DonationDto donationDto);
}