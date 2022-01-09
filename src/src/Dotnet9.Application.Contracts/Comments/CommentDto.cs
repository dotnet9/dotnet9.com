using System;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Comments;

public class CommentDto : EntityDto<Guid>
{
    public string Text { get; set; }

    public Guid? RepliedCommentId { get; set; }
}