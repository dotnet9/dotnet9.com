namespace Dotnet9.Application.Role.Dtos;

public class UpdateSysRoleInput : AddSysRoleInput
{
    /// <summary>
    /// 角色Id
    /// </summary>
    public long Id { get; set; }
}