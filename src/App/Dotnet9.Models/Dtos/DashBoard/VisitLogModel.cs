namespace Dotnet9.Models.Dtos.DashBoard;

public class VisitLogModel
{
    public string Ip { get; set; }

    /// <summary>
    ///     来源城市
    /// </summary>
    public string SourceCity { get; set; }

    /// <summary>
    ///     访问时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    ///     浏览器
    /// </summary>
    public string Browser { get; set; }

    /// <summary>
    ///     操作系统
    /// </summary>
    public string Os { get; set; }
}