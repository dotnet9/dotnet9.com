using System.Diagnostics.CodeAnalysis;
using Dotnet9.Core;
using Dotnet9.Domain.Shared.Tags;

namespace Dotnet9.Domain.UrlLinks;

public class UrlLink : EntityBase
{
    private UrlLink()
    {
    }

    internal UrlLink(int id, int index, UrlLinkKind kind, string name, string? description, string url) : base(id)
    {
        Index = index;
        Kind = kind;
        SetName(name);
        Description = description;
        SetUrl(url);
    }

    public string Name { get; private set; } = null!;
    public string Url { get; set; } = null!;
    public string? Description { get; set; }
    public UrlLinkKind Kind { get; set; }
    public int Index { get; set; }

    public override string ToString()
    {
        return $"id: {Id}, kind: {Kind}, name: {Name}, url: {Url}, description: {Description}";
    }

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
        Url = Check.NotNullOrWhiteSpace(url, nameof(url), TagConsts.MaxUrlLength);
    }
}