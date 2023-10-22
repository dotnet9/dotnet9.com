namespace Dotnet9.Application.Menu.Dtos;

public class SysMenuDetailOutput
{
    /// <summary>
    /// 菜单Id
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 菜单名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 父级id
    /// </summary>
    public long? ParentId { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }
    /// <summary>
    /// 权限标识
    /// </summary>
    public string Code { get; set; }
    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }
    /// <summary>
    /// 组件路径
    /// </summary>
    public string Component { get; set; }
    /// <summary>
    /// 图标
    /// </summary>
    public string Icon { get; set; }
    /// <summary>
    /// 是否固定
    /// </summary>
    public bool IsFixed { get; set; }
    /// <summary>
    /// 是否内嵌
    /// </summary>
    public bool IsIframe { get; set; }
    /// <summary>
    /// 是否缓存
    /// </summary>
    public bool IsKeepAlive { get; set; }
    /// <summary>
    /// 是否可见
    /// </summary>
    public bool IsVisible { get; set; }
    /// <summary>
    /// 外链
    /// </summary>
    public string Link { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
    /// <summary>
    /// 路由地址
    /// </summary>
    public string Path { get; set; }
    /// <summary>
    /// 重定向地址
    /// </summary>
    public string Redirect { get; set; }
    /// <summary>
    /// 路由名称
    /// </summary>
    public string RouteName { get; set; }
    /// <summary>
    /// 菜单类型
    /// </summary>
    public MenuType Type { get; set; }
}