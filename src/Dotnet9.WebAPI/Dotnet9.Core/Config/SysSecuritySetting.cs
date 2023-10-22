
using System;
using System.Collections.Generic;
namespace Dotnet9.Core.Config;
public class SysSecuritySetting
{
    /// <summary>
    /// 用户默认密码
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// 密码错误次数
    /// </summary>
    public int Times { get; set; }
    /// <summary>
    /// 密码规则
    /// </summary>
    public string PasswordRule { get; set; }
    /// <summary>
    /// 密码规则提示语
    /// </summary>
    public string RuleMessage { get; set; }
    /// <summary>
    /// Token有效期(分钟)
    /// </summary>
    public int? TokenExpired { get; set; }
}
