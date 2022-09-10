namespace Dotnet9.WebAPI.Domain.Abouts;

public class AboutDomainService
{
    private readonly IAboutRepository _repository;

    public AboutDomainService(IAboutRepository repository)
    {
        _repository = repository;
    }

    public async Task<About> AddAboutAsync(string content)
    {
        var id = Guid.NewGuid();
        return new About(id, content);
    }
}