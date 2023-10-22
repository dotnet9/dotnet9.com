namespace Dotnet9.Application.Client.Dtos;

public class CommentPageQueryInput : Pagination
{
    /// <summary>
    /// 对应模块ID或评论ID（null表留言，0代表友链的评论）
    /// </summary>
    public long? Id { get; set; }
}