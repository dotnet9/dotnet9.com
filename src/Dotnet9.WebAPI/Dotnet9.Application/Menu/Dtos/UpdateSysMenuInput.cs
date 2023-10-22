namespace Dotnet9.Application.Menu.Dtos;

public class UpdateSysMenuInput : AddSysMenuInput
{
    /// <summary>
    /// 菜单/按钮Id
    /// </summary>
    public long Id { get; set; }
}