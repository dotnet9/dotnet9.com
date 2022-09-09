namespace Dotnet9.WebAPI.Domain.Tags;

public record Tag : AggregateRootEntity
{
    private Tag()
    {
    }

    internal Tag(Guid id, string name)
    {
        Id = id;
        ChangeName(name);
    }

    public string? Name { get; private set; }

    public Tag ChangeName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), TagConsts.MaxNameLength,
            TagConsts.MinNameLength);
        return this;
    }
}