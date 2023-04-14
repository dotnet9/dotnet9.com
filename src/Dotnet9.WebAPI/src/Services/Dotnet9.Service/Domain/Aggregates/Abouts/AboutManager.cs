namespace Dotnet9.Service.Domain.Aggregates.Abouts;

public class AboutManager : IScopedDependency
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