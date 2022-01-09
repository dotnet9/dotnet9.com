using System;
using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Comments;

public class CreateCommentDto
{
    [Required]
    [StringLength(CommentConsts.MaxTextLength)]
    public string Text { get; set; }

    public Guid? RepliedCommentId { get; set; }
}