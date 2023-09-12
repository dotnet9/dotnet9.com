using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Services.Config;

public class SiteConfig
{
    [Required] public string SiteName { get; set; }

    [Required] public string ICP { get; set; }

    /// <summary>
    ///     站点描述
    /// </summary>
    public string SiteDesc { get; set; }
}