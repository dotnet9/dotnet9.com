namespace Dotnet9.Commons;

public class SiteOptions
{
    public string? AppName { get; set; }
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
    public int Start { get; set; }
    public string? Bilibili { get; set; }
    public string? Zhihu { get; set; }
}