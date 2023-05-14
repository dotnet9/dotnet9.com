namespace Dotnet9.Service.Domain.Repositories;

public interface IPrivacyRepository : IRepository<Privacy, Guid>
{
    Task<Privacy?> GetAsync();
}
