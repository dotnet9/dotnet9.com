namespace Dotnet9.Application.Menu.Dtos;

public class AddSysMenuInput
{
    /// <summary>
    /// 菜单名称
    /// </summary>
    [MaxLength(32, ErrorMessage = "名称限制32个字符内")]
    [Required(ErrorMessage = "名称为必填项")]
    public string Name { get; set; }

    /// <summary>
    /// 菜单类型
    /// </summary>
    public MenuType Type { get; set; }

    /// <summary>
    /// 权限编码
    /// </summary>
    [MaxLength(128, ErrorMessage = "权限编码限制128字符内")]
    public string Code { get; set; }

    /// <summary>
    /// 父级菜单
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 路由名
    /// </summary>
    [MaxLength(32, ErrorMessage = "路由名称制32个字符内")]
    public string RouteName { get; set; }

    /// <summary>
    /// 路由地址
    /// </summary>
    [MaxLength(256, ErrorMessage = "路由地址限制256个字符内")]
    public string Path { get; set; }

    /// <summary>
    /// 组件路径
    /// </summary>
    [MaxLength(128, ErrorMessage = "组件路径限制128个字符内")]
    public string Component { get; set; }

    /// <summary>
    /// 重定向地址
    /// </summary>
    [MaxLength(128, ErrorMessage = "重定向地址限制256个字符")]
    public string Redirect { get; set; }

    /// <summary>
    /// 菜单图标
    /// </summary>
    [MaxLength(64, ErrorMessage = "菜单图标显示64个字符内")]
    public string Icon { get; set; }

    /// <summary>
    /// 是否内嵌页面
    /// </summary>
    public bool IsIframe { get; set; }

    /// <summary>
    /// 外链地址
    /// </summary>
    [MaxLength(256, ErrorMessage = "外链地址限制256个字符内")]
    public string Link { get; set; }

    /// <summary>
    /// 是否可见
    /// </summary>
    public bool IsVisible { get; set; }

    /// <summary>
    /// 是否缓存
    /// </summary>
    public bool IsKeepAlive { get; set; }

    /// <summary>
    /// 是否固定
    /// </summary>
    public bool IsFixed { get; set; }

    /// <summary>
    /// 可用状态
    /// </summary>
    public AvailabilityStatus Status { get; set; }

    /// <summary>
    /// 排序值（值越小越靠前）
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(256, ErrorMessage = "备注限制200个字符内")]
    public string Remark { get; set; }
}