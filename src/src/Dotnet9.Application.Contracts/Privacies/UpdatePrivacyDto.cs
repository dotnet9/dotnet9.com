using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Privacies;

public class UpdatePrivacyDto
{
    [Required]
    [StringLength(PrivacyConsts.MaxDetailsLength)]
    public string Details { get; set; }
}