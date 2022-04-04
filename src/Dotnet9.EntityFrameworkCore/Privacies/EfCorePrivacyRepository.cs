using Dotnet9.Domain.Privacies;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.Privacies;

public class EfCorePrivacyRepository : EfCoreRepository<Privacy>, IPrivacyRepository
{
    public EfCorePrivacyRepository(Dotnet9DbContext context) : base(context)
    {
    }
}