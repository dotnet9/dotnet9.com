using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.Abouts;

public class EfCoreAboutRepository : EfCoreRepository<About>, IAboutRepository
{
    public EfCoreAboutRepository(Dotnet9DbContext context) : base(context)
    {
    }
}