using Dotnet9.Core.Enum;

namespace Dotnet9.Core.Entities;

/// <summary>
/// 系统菜单表
/// </summary>
public class SysMenu : Entity<long>, IAvailability, ISortable, ICreatedUserId, ISoftDelete, ICreatedTime
{
    /// <summary>
    /// 菜单名称
    /// </summary>
    [SugarColumn(Length = 32)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 菜单类型
    /// </summary>
    public MenuType Type { get; set; }

    /// <summary>
    /// 权限编码
    /// </summary>
    [SugarColumn(Length = 128)]
    public string? Code { get; set; }

    /// <summary>
    /// 父级菜单
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 路由名
    /// </summary>
    [SugarColumn(Length = 32)]
    public string? RouteName { get; set; }

    /// <summary>
    /// 路由地址
    /// </summary>
    [SugarColumn(Length = 128)]
    public string? Path { get; set; }

    /// <summary>
    /// 组件路径
    /// </summary>
    [SugarColumn(Length = 128)]
    public string? Component { get; set; }

    /// <summary>
    /// 重定向地址
    /// </summary>
    [SugarColumn(Length = 128)]
    public string? Redirect { get; set; }

    /// <summary>
    /// 菜单图标
    /// </summary>
    [SugarColumn(Length = 64)]
    public string? Icon { get; set; }

    /// <summary>
    /// 是否内嵌页面
    /// </summary>
    public bool IsIframe { get; set; }

    /// <summary>
    /// 外链地址
    /// </summary>
    [SugarColumn(Length = 256)]
    public string? Link { get; set; }

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
    [SugarColumn(Length = 256)]
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public long CreatedUserId { get; set; }

    /// <summary>
    /// 标记删除
    /// </summary>
    public bool DeleteMark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; }

    /// <summary>
    /// 子菜单
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public List<SysMenu> Children { get; set; } = new();
}