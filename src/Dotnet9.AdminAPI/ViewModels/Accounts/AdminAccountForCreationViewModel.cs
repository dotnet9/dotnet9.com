using System.ComponentModel.DataAnnotations;

namespace Dotnet9.AdminAPI.ViewModels.Accounts;

public class AdminAccountForCreationViewModel
{
    [Required] public string Account { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;
}