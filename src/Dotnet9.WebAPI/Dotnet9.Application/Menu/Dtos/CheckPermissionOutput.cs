namespace Dotnet9.Application.Menu.Dtos;

public class CheckPermissionOutput
{
    /// <summary>
    /// 权限标识
    /// </summary>
    public string Code { get; set; }
    /// <summary>
    /// 是否有访问权限
    /// </summary>
    public bool Access { get; set; }
}