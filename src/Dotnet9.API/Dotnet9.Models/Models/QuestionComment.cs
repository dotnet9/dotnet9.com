using Dotnet9.Models.Models.RootTKeys;

namespace Dotnet9.Models.Models;

public class QuestionComment : RootEntityTkey<int>
{
    public int QuestionId { get; set; }

    public virtual Question? Question { get; set; }

    public string? Content { get; set; }

    public bool IsAdoption { get; set; }

    public int CreateUserId { get; set; }

    public virtual UserInfo? CreateUser { get; set; }

    public DateTime CreateTime { get; set; }

    public int UpdateUserId { get; set; }

    public virtual UserInfo? UpdateUser { get; set; }

    public DateTime UpdateTime { get; set; }
}