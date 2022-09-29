namespace Dotnet9.WebAPI.Domain.Abouts;

public class AboutManager
{
    private readonly IAboutRepository _repository;

    public AboutManager(IAboutRepository repository)
    {
        _repository = repository;
    }

    public About Create(string content)
    {
        var id = Guid.NewGuid();
        return new About(id, content);
    }
}