using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.Donations;

public class EfCoreDonationRepository : EfCoreRepository<Donation>, IDonationRepository
{
    public EfCoreDonationRepository(Dotnet9DbContext context) : base(context)
    {
    }
}