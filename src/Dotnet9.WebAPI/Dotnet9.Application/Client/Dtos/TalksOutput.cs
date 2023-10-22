namespace Dotnet9.Application.Client.Dtos;

public class TalksOutput
{
    /// <summary>
    /// 
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 是否置顶
    /// </summary>
    public bool IsTop { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    public string Content { get; set; }
    /// <summary>
    /// 图片
    /// </summary>
    public string Images { get; set; }
    /// <summary>
    /// 是否已点赞
    /// </summary>
    public bool IsPraise { get; set; }
    /// <summary>
    /// 点赞数量
    /// </summary>
    public int Upvote { get; set; }
    /// <summary>
    /// 评论数量
    /// </summary>
    public int Comments { get; set; }
    /// <summary>
    /// 发布时间
    /// </summary>
    public DateTime CreatedTime { get; set; }
}