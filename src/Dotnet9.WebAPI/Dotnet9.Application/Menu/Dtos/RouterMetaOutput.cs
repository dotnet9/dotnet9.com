namespace Dotnet9.Application.Menu.Dtos;

public class RouterMetaOutput
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// 外链
    /// </summary>
    public string IsLink { get; set; }
    /// <summary>
    /// 是否隐藏
    /// </summary>
    public bool IsHide { get; set; }
    /// <summary>
    /// 是否缓存
    /// </summary>
    public bool IsKeepAlive { get; set; }
    /// <summary>
    /// 是否固定
    /// </summary>
    public bool IsAffix { get; set; }

    /// <summary>
    /// 菜单
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// 类型
    /// </summary>
    public MenuType Type { get; set; }
}