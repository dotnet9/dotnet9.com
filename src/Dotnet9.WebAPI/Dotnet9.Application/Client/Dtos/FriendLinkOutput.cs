namespace Dotnet9.Application.Client.Dtos;

public class FriendLinkOutput
{
    /// <summary>
    /// 友链ID
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 友链
    /// </summary>
    public string Link { get; set; } = null!;
    /// <summary>
    /// logo
    /// </summary>
    public string Logo { get; set; } = null!;
    /// <summary>
    /// 站点名称
    /// </summary>
    public string SiteName { get; set; } = null!;
    /// <summary>
    /// 网站描述
    /// </summary>
    public string Remark { get; set; } = null!;
}