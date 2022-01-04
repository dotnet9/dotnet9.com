using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Abouts;

public class UpdateAboutDto
{
    [Required]
    [StringLength(AboutConsts.MaxDetailsLength)]
    public string Details { get; set; }
}