namespace Dotnet9.Service.Application.Abouts;

public class AboutHandler
{
    private readonly IAboutRepository _repository;

    public AboutHandler(IAboutRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetAsync(AboutQuery query, CancellationToken cancellationToken)
    {
        var about = await _repository.GetAsync();

        query.Result = about?.Map<AboutDto>();
    }
}