using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Comments;

public class Comment : FullAuditedAggregateRoot<Guid>
{
    [NotNull] public virtual string Text { get; protected set; }

    public virtual Guid? RepliedCommentId { get; protected set; }
}