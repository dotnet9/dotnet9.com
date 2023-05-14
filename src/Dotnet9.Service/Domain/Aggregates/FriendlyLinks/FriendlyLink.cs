namespace Dotnet9.Service.Domain.Aggregates.FriendlyLinks;

public class FriendlyLink : FullAggregateRoot<Guid, int>
{
    private FriendlyLink()
    {
    }

    internal FriendlyLink(Guid id, int index, string name, string url, string? description) : base(id)
    {
        ChangeIndex(index);
        ChangeName(name);
        ChangeUrl(url);
        ChangeDescription(description);
    }
    
    public int Index { get; private set; }
    
    public string Name { get; private set; } = default!;
    
    public string Url { get; private set; } = default!;
    
    public string? Description { get; private set; }
    
    public FriendlyLink ChangeIndex(int index)
    {
        Index = index;
        return this;
    }

    internal FriendlyLink ChangeName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), FriendlyLinkConsts.MaxNameLength,
            FriendlyLinkConsts.MinNameLength);
        return this;
    }

    internal FriendlyLink ChangeUrl(string url)
    {
        Url = Check.NotNullOrWhiteSpace(url, nameof(url), FriendlyLinkConsts.MaxUrlLength,
            FriendlyLinkConsts.MinUrlLength);
        return this;
    }

    internal FriendlyLink ChangeDescription(string? description)
    {
        Description = Check.Length(description, nameof(description), FriendlyLinkConsts.MaxDescriptionLength);
        return this;
    }
}