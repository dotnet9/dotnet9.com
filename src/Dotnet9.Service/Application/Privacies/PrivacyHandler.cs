namespace Dotnet9.Service.Application.Privacies;

public class PrivacyHandler
{
    private readonly IPrivacyRepository _repository;

    public PrivacyHandler(IPrivacyRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetAsync(PrivacyQuery query, CancellationToken cancellationToken)
    {
        var about = await _repository.GetAsync();

        query.Result = about?.Map<PrivacyDto>();
    }
}