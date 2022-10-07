namespace Dotnet9.Application.Contracts.Abouts;

public interface IAboutAppService
{
    Task<AboutDto?> GetAsync();
    Task<bool> UpdateAsync(AboutDto aboutDto);
}