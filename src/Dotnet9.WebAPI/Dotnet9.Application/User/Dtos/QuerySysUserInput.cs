namespace Dotnet9.Application.User.Dtos;

public class QuerySysUserInput : Pagination
{
    /// <summary>
    /// 账号
    /// </summary>
    public string Account { get; set; }

    /// <summary>
    /// 组织机构Id
    /// </summary>
    public long? OrgId { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Mobile { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; }
}