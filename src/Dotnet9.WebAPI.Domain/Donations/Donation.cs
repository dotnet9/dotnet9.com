namespace Dotnet9.WebAPI.Domain.Donations;

public record Donation : AggregateRootEntity
{
    private Donation()
    {
    }

    internal Donation(Guid id, string content)
    {
        Id = id;
        ChangeContent(content);
    }

    public string? Content { get; private set; }

    public Donation ChangeContent(string content)
    {
        Content = Check.NotNullOrWhiteSpace(content, nameof(content));
        return this;
    }
}