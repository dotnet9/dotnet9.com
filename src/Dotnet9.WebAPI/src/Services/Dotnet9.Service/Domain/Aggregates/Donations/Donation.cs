namespace Dotnet9.Service.Domain.Aggregates.Donations;

public class Donation : FullAggregateRoot<Guid, int>
{
    private Donation()
    {
    }

    internal Donation(Guid id, string content)
    {
        Id = id;
        ChangeContent(content);
    }

    public string Content { get; private set; } = null!;

    public Donation ChangeContent(string content)
    {
        Content = Check.NotNullOrWhiteSpace(content, nameof(content), DonationConsts.MaxContentLength,
            DonationConsts.MinContentLength);
        return this;
    }
}