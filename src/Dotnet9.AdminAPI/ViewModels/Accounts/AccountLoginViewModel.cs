using System.ComponentModel.DataAnnotations;

namespace Dotnet9.AdminAPI.ViewModels.Accounts;

public class AccountLoginViewModel
{
    [Required] public string Account { get; set; } = null!;

    [Required] public string Password { get; set; } = null!;
}