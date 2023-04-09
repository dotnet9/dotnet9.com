namespace Dotnet9.Service.Domain.Repositories;

public interface IPrivacyRepository
{
    Task<Privacy?> GetAsync();
}