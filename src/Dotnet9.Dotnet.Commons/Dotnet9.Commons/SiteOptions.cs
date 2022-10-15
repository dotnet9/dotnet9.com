using System.Security.Principal;

namespace Dotnet9.Commons;

public class SiteOptions
{
    public string? AppName { get; set; }
    public string? Subheading { get; set; }
    public string? Description { get; set; }
    public string Domain { get; set; } = null!;
    public string? BeiAn { get; set; }
    public string? BeiAnUrl { get; set; }
    public string? Logo { get; set; }
    public string? Github { get; set; }

    public string AssetsLocalPath { get; set; } = null!;

    public string AssetsRemotePath { get; set; } = null!;
    public string Owner { get; set; } = null!;
    public string? OwnerAvatar { get; set; }
    public string? OwnerDesc { get; set; }
    public string? WechatPublic1Name { get; set; }
    public string? WechatPublic1 { get; set; }
    public string? WechatPublic2Name { get; set; }
    public string? WechatPublic2 { get; set; }
    public int Start { get; set; }
    public string? Bilibili { get; set; }
    public string? Zhihu { get; set; }
    public bool Valine { get; set; }
    public ValineConfig? ValineConfig { get; set; }
}

public class ValineConfig
{
    public string? AppId { get; set; }
    public string? AppKey { get; set; }
    public string? ServerURL { get; set; }
    public string? Placeholder { get; set; }
}