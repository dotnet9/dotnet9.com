
using System;
using System.Collections.Generic;
namespace Dotnet9.Core.Config;
public class BlogSetting
{
    /// <summary>
    /// 网站Logo
    /// </summary>
    public string Logo { get; set; } = null!;
    /// <summary>
    /// 站点图标
    /// </summary>
    public string Favicon { get; set; } = null!;
    /// <summary>
    /// 开启打赏
    /// </summary>
    public bool? IsRewards { get; set; }
    /// <summary>
    /// 支付宝
    /// </summary>
    public string AliPay { get; set; } = null!;
    /// <summary>
    /// 微信收款码
    /// </summary>
    public string WxPay { get; set; } = null!;
    /// <summary>
    /// 允许留言
    /// </summary>
    public bool? IsAllowMessage { get; set; }
    /// <summary>
    /// 允许评论
    /// </summary>
    public bool? IsAllowComments { get; set; }
    /// <summary>
    /// 公告
    /// </summary>
    public string Announcement { get; set; } = null!;
    /// <summary>
    /// 站点名称
    /// </summary>
    public string SiteName { get; set; } = null!;
    /// <summary>
    /// 首页格言
    /// </summary>
    public string Motto { get; set; } = null!;
    /// <summary>
    /// 网站运营时间
    /// </summary>
    public DateTime RunTime { get; set; }
    /// <summary>
    /// 站点版权
    /// </summary>
    public string Copyright { get; set; } = null!;
    /// <summary>
    /// 站点描述
    /// </summary>
    public string Description { get; set; } = null!;
    /// <summary>
    /// 关键词
    /// </summary>
    public string Keyword { get; set; } = null!;
    /// <summary>
    /// 备案号
    /// </summary>
    public string Filing { get; set; } = null!;
}
