using Dotnet9.Core;
using Dotnet9.Domain.Shared.Tags;

namespace Dotnet9.Domain.Tags;

public class Tag : EntityBase
{
    private Tag()
    {
    }

    internal Tag(int id, string name) : base(id)
    {
        SetName(name);
    }

    public string Name { get; private set; } = null!;

    internal Tag ChangeName(string name)
    {
        SetName(name);
        return this;
    }

    private void SetName(string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), TagConsts.MaxNameLength);
    }
}