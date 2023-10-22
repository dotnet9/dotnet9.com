namespace Dotnet9.Core.Entities;

/// <summary>
/// 角色权限表
/// </summary>
public class SysRoleMenu : Entity<long>
{
    /// <summary>
    /// 角色id
    /// </summary>
    public long RoleId { get; set; }

    /// <summary>
    /// 菜单按钮id
    /// </summary>
    public long MenuId { get; set; }
}