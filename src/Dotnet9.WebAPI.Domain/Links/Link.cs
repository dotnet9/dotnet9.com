namespace Dotnet9.WebAPI.Domain.Links;

public record Link : AggregateRootEntity
{
    private Link()
    {
    }

    internal Link(
        Guid id,
        int sequenceNumber,
        string name,
        string url,
        string? description = null,
        LinkKind kind = LinkKind.Friend)
    {
        Id = id;
        ChangeSequenceNumber(sequenceNumber);
        ChangeName(name);
        ChangeUrl(url);
        ChangeDescription(description);
        ChangeKind(kind);
    }

    public int SequenceNumber { get; private set; }
    public string Name { get; private set; } = null!;
    public string Url { get; private set; } = null!;
    public string? Description { get; private set; }
    public LinkKind Kind { get; private set; }


    public Link ChangeSequenceNumber(int sequenceNumber)
    {
        SequenceNumber = sequenceNumber;
        return this;
    }

    internal Link ChangeName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), LinkConsts.MaxNameLength,
            LinkConsts.MinNameLength);
        return this;
    }

    internal Link ChangeUrl(string url)
    {
        Url = Check.NotNullOrWhiteSpace(url, nameof(url), LinkConsts.MaxUrlLength,
            LinkConsts.MinUrlLength);
        return this;
    }

    internal Link ChangeDescription(string? description)
    {
        Description = Check.Length(description, nameof(description), LinkConsts.MaxDescriptionLength);
        return this;
    }

    internal Link ChangeKind(LinkKind kind)
    {
        Kind = kind;
        return this;
    }
}