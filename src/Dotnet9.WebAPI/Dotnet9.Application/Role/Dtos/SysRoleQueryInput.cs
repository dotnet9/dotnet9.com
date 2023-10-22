namespace Dotnet9.Application.Role.Dtos;

public class SysRoleQueryInput : Pagination
{
    /// <summary>
    /// 角色名称
    /// </summary>
    public string Name { get; set; }
}