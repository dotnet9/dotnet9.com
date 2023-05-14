namespace Dotnet9.Contracts.Dto;

public class SiteInfo
{
    public string? AppName { get; set; }
    public string? Keywords { get; set; }
    public string? Subheading { get; set; }
    public string? Description { get; set; }
    public string Domain { get; set; } = null!;
    public string? BeiAn { get; set; }
    public string? BeiAnUrl { get; set; }
    public string? Logo { get; set; }
    public string? Github { get; set; }
    public string? Gitee { get; set; }
    public string? ApiService { get; set; }
    public string AssetsLocalPath { get; set; } = null!;

    public string AssetsRemotePath { get; set; } = null!;
    public string Owner { get; set; } = null!;
    public string? OwnerAvatar { get; set; }
    public string? OwnerDesc { get; set; }
    public string? OwnerWechat { get; set; }
    public string? WechatPublic1Name { get; set; }
    public string? WechatPublic1 { get; set; }
    public string? WechatPublic2Name { get; set; }
    public string? WechatPublic2 { get; set; }
    public string? QQ { get; set; }
    public string? CSDN { get; set; }
    public string? JueJin { get; set; }
    public string? ZhiHu { get; set; }
    public int Start { get; set; }
    public string? WebsiteCreateTime { get; set; }
    public string? Bilibili { get; set; }
    public int IsCommentReview { get; set; }
    public int MultiLanguage { get; set; }
}