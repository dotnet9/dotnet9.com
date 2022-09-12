namespace Dotnet9.WebAPI.Domain.Donations;

public class DonationManager
{
    private readonly IDonationRepository _repository;

    public DonationManager(IDonationRepository repository)
    {
        _repository = repository;
    }

    public Donation Create(string content)
    {
        var id = Guid.NewGuid();
        return new Donation(id, content);
    }
}