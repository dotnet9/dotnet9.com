using System.ComponentModel.DataAnnotations;

namespace Dotnet9.Web.ViewModels.Accounts;

public class AccountLoginViewModel
{
    [Required] public string Account { get; set; } = null!;

    [Required] public string Password { get; set; } = null!;
}