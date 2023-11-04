namespace Dotnet9.Application.Client.Dtos;

public class OAuthAccountDetailOutput
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; } = null!;
    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; } = null!;
    /// <summary>
    /// 状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }
    /// <summary>
    /// 网站链接
    /// </summary>
    public string? Link { get; set; }
    /// <summary>
    /// logo 
    /// </summary>
    public string? Logo { get; set; }
    /// <summary>
    /// 网站名称
    /// </summary>
    public string? SiteName { get; set; }
    /// <summary>
    /// 对方博客友链地址
    /// </summary>
    public string? Url { get; set; }
    /// <summary>
    /// 网站介绍
    /// </summary>
    public string? Remark { get; set; }
}