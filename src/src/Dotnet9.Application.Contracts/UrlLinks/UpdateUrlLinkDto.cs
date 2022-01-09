using System.ComponentModel.DataAnnotations;

namespace Dotnet9.UrlLinks;

public class UpdateUrlLinkDto
{
    [Required]
    [StringLength(UrlLinkConsts.MaxNameLength)]
    public string Name { get; set; }

    [Required]
    [StringLength(UrlLinkConsts.MaxUrlLength)]
    public string Url { get; set; }

    [StringLength(UrlLinkConsts.MaxDescriptionLength)]
    public string Description { get; set; }

    public int Index { get; set; }
}