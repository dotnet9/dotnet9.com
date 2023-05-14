namespace Dotnet9.Service.Domain.Repositories;

public interface IDonationRepository : IRepository<Donation, Guid>
{
    Task<Donation?> GetAsync();
}