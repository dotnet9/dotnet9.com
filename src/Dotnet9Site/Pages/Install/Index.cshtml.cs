using Dotnet9.Services.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Dotnet9Site.Pages.Install;

public class Index : PageModel
{
    private AccountService _accountService;

    public bool IsConnect = true;


    public Index(AccountService accountService)
    {
        _accountService = accountService;
    }

    public SiteInitModel SiteInitModel { get; set; }

    public async Task<IActionResult> OnGet()
    {
        bool isInit = await _accountService.CheckSysInitStatus();
        if (isInit)
        {
            return Redirect("/");
        }

        return Page();
    }
}

public class SiteInitModel
{
    /// <summary>
    ///     数据库连接
    /// </summary>
    [Display(Name = "数据库连接字符串")]
    [Required(ErrorMessage = "这个字段是必须的")]
    public string DbConnectString { get; set; }

    /// <summary>
    ///     用户名
    /// </summary>
    [Display(Name = "用户名")]
    [Required(ErrorMessage = "用户名不能为空")]
    public string UserName { get; set; }

    /// <summary>
    ///     用户密码
    /// </summary>
    [Display(Name = "密码")]
    [Required(ErrorMessage = "密码不能为空")]
    public string UserPwd { get; set; }
}