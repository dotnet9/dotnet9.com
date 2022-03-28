using System.Diagnostics.CodeAnalysis;
using Dotnet9.Core;
using Dotnet9.Domain.Shared.Tags;

namespace Dotnet9.Domain.UrlLinks;

public class UrlLink : EntityBase
{
    private UrlLink()
    {
    }

    internal UrlLink(int id, [NotNull] string name, [NotNull] string url, string? description = "",
        UrlKind kind = UrlKind.Friendly, int index = 0) : base(id)
    {
        SetName(name);
        SetUrl(url);
        Description = description;
        Kind = kind;
        Index = index;
    }

    public string Name { get; private set; } = null!;
    public string Url { get; } = null!;
    public string? Description { get; set; }
    public UrlKind Kind { get; set; }
    public int Index { get; set; }

    internal UrlLink ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), TagConsts.MaxNameLength);
    }

    internal UrlLink ChangeUrl([NotNull] string url)
    {
        SetUrl(url);
        return this;
    }

    private void SetUrl([NotNull] string url)
    {
        Name = Check.NotNullOrWhiteSpace(url, nameof(url), TagConsts.MaxUrlLength);
    }
}