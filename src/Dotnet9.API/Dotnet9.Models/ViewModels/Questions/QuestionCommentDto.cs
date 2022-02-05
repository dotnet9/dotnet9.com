namespace Dotnet9.Models.ViewModels.Questions;

public class QuestionCommentDto
{
    public string? Content { get; set; }

    public bool IsAdoption { get; set; }

    public int CreateUserId { get; set; }

    public string? CreateUserName { get; set; }

    public string? CreateUserPortrait { get; set; }

    public DateTime CreateTime { get; set; }

    public int UpdateUserId { get; set; }

    public string? UpdateUserName { get; set; }

    public string? UpdateUserPortrait { get; set; }

    public DateTime UpdateTime { get; set; }
}