namespace Dotnet9.Application.User.Dtos;

public class SysUserInfoOutput
{
    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 账户名
    /// </summary>
    public string Account { get; set; }
    /// <summary>
    /// 头像
    /// </summary>
    public string Avatar { get; set; }
    /// <summary>
    /// 生日
    /// </summary>
    public DateOnly? Birthday { get; set; }
    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// 性别
    /// </summary>
    public Gender Gender { get; set; }
    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
    /// <summary>
    /// 最后登录ip
    /// </summary>
    public string LastLoginIp { get; set; }
    /// <summary>
    /// 最后登录IP所属地址
    /// </summary>
    public string LastLoginAddress { get; set; }
    /// <summary>
    /// 手机号码
    /// </summary>
    public string Mobile { get; set; }
    /// <summary>
    /// 机构id
    /// </summary>
    public long OrgId { get; set; }
    /// <summary>
    /// 机构名称
    /// </summary>
    public string OrgName { get; set; }
    /// <summary>
    /// 授权按钮
    /// </summary>
    public List<string> AuthBtnList { get; set; }
}