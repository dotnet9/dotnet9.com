namespace Dotnet9.Service.Application.Donations;

public class DonationHandler
{
    private readonly IDonationRepository _repository;

    public DonationHandler(IDonationRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetAsync(DonationQuery query, CancellationToken cancellationToken)
    {
        var about = await _repository.GetAsync();

        query.Result = about?.Map<DonationDto>();
    }
}