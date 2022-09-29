namespace Dotnet9.WebAPI.Domain.Privacies;

public interface IPrivacyRepository
{
    Task<Privacy?> GetAsync();
}