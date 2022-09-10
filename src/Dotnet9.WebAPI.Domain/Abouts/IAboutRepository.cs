namespace Dotnet9.WebAPI.Domain.Abouts;

public interface IAboutRepository
{
    Task<About?> GetAboutAsync();
}