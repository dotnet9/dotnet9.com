namespace Dotnet9.Models.ViewModels.Questions;

public class QuestionDto
{
    public string? Title { get; set; }

    public int CommentCount { get; set; }

    public string? Categories { get; set; }

    public string? Tags { get; set; }

    public int Traffic { get; set; }

    public int CreateUserId { get; set; }

    public DateTime CreateTime { get; set; }

    public int UpdateUserId { get; set; }

    public DateTime UpdateTime { get; set; }
}