namespace Dotnet9.Application.Menu.Dtos;

public class RouterOutput
{
    /// <summary>
    /// 路由名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 路由地址
    /// </summary>
    public string Path { get; set; }
    /// <summary>
    /// 组件
    /// </summary>
    public string Component { get; set; }

    /// <summary>
    /// 路由扩展信息
    /// </summary>
    public RouterMetaOutput Meta { get; set; }

    /// <summary>
    /// 子菜单
    /// </summary>
    public List<RouterOutput> Children { get; set; } = new();
}