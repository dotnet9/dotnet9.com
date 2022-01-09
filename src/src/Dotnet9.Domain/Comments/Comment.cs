using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Domain.Entities.Auditing;

namespace Dotnet9.Comments;

public class Comment : FullAuditedAggregateRoot<Guid>
{
    [NotNull] public string Text { get; set; }

    public Guid? RepliedCommentId { get; set; }
}