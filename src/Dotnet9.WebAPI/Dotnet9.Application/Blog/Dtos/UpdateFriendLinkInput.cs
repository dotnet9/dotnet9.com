namespace Dotnet9.Application.Blog.Dtos;

public class UpdateFriendLinkInput : AddFriendLinkInput
{
    /// <summary>
    /// 友链主键
    /// </summary>
    [Required(ErrorMessage = "缺少必要参数")]
    public long Id { get; set; }
}