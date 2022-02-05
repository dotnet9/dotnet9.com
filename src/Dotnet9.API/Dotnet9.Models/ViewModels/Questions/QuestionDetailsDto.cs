namespace Dotnet9.Models.ViewModels.Questions;

public class QuestionDetailsDto
{
    public string? Title { get; set; }

    public string? Categories { get; set; }

    public string? Tags { get; set; }

    public int CommentCount { get; set; }

    public virtual List<QuestionCommentDto>? QuestionComments { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime UpdateTime { get; set; }
}