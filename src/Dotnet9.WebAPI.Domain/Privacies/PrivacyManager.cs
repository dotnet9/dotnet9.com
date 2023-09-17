namespace Dotnet9.WebAPI.Domain.Privacies;

public class PrivacyManager
{
    private readonly IPrivacyRepository _repository;

    public PrivacyManager(IPrivacyRepository repository)
    {
        _repository = repository;
    }

    public Privacy Create(string content)
    {
        var id = Guid.NewGuid();
        return new Privacy(id, content);
    }
}