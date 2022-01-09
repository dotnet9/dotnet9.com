using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.UrlLinks;

public class UrlLink : FullAuditedAggregateRoot<Guid>
{
    private UrlLink()
    {
    }


    internal UrlLink(
        Guid id,
        [NotNull] string name,
        [NotNull] string url,
        string description,
        int index) : base(id)
    {
        SetName(name);
        SetUrl(url);
        Description = description;
        Index = index;
    }

    internal UrlLink ChangeName([NotNull] string name)
    {
        SetName(name);
        return this;
    }

    private void SetName([NotNull] string name)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name), UrlLinkConsts.MaxNameLength);
    }

    internal UrlLink ChangeUrl([NotNull] string url)
    {
        SetUrl(url);
        return this;
    }

    private void SetUrl([NotNull] string url)
    {
        Url = Check.NotNullOrWhiteSpace(url, nameof(url), UrlLinkConsts.MaxUrlLength);
    }

    [NotNull] public string Name { get; set; }

    [NotNull] public string Url { get; set; }

    public string Description { get; set; }

    public int Index { get; set; }
}