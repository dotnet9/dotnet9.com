namespace Dotnet9.Application.Contracts.Privacies;

public interface IPrivacyAppService
{
    Task<PrivacyDto?> GetAsync();
    Task<bool> UpdateAsync(PrivacyDto privacyDto);
}