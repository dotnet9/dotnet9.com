namespace Dotnet9.Commons;

public class SiteOptions
{
    public string Domain { get; set; } = null!;

    public string AssetsLocalPath { get; set; } = null!;

    public string AssetsRemotePath { get; set; } = null!;
    public string Owner { get; set; } = null!;
    public int Start { get; set; }
}