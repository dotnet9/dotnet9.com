namespace Dotnet9.Service.Domain.Aggregates.Tags;

public class Tag : FullAggregateRoot<Guid, int>
{
    private Tag()
    {
    }

    internal Tag(Guid id, string name)
    {
        Id = id;
        ChangeName(name);
    }

    public string Name { get; private set; } = null!;

    public Tag ChangeName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), TagConsts.MaxNameLength,
            TagConsts.MinNameLength);
        return this;
    }
}