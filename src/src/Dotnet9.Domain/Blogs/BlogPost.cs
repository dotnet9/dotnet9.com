using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Blogs;

public class BlogPost : FullAuditedAggregateRoot<Guid>
{
    [NotNull] public virtual string Title { get; protected set; }

    [NotNull] public virtual string ShortDescription { get; protected set; }

    [NotNull] public virtual string Content { get; protected set; }

    [NotNull] public virtual string CoverImageUrl { get; protected set; }

    // Used for EF Core
    public int BlogCopyrightType { get; set; }

    // Used for code
    [NotMapped]
    public CopyrightType? BlogCopyrightTypeEnum
    {
        get => (CopyrightType?)Enum.Parse(typeof(CopyrightType), BlogCopyrightType.ToString());
        set
        {
            if (value.HasValue)
            {
                BlogCopyrightType = (int)value.Value;
            }
            else
            {
                BlogCopyrightType = (int)CopyrightType.Default;
            }
        }
    }

	[NotNull] public virtual string Original { get; protected set; }

    [NotNull] public virtual string OriginalTitle { get; protected set; }

    [NotNull] public virtual string OriginalLink { get; protected set; }
}