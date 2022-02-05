using Dotnet9.Models.Models.RootTKeys;

namespace Dotnet9.Models.Models;

public class Question : RootEntityTkey<int>
{
    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? Categories { get; set; }

    public string? Tags { get; set; }

    public int Traffic { get; set; }

    public virtual ICollection<QuestionComment>? QuestionComments { get; set; }

    public int CreateUserId { get; set; }

    public virtual UserInfo? CreateUser { get; set; }

    public DateTime CreateTime { get; set; }

    public int UpdateUserId { get; set; }

    public virtual UserInfo? UpdateUser { get; set; }

    public DateTime UpdateTime { get; set; }
}