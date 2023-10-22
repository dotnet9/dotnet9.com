namespace SqlSugar;

public interface IPagination
{
    /// <summary>
    /// 当前页码
    /// </summary>
    int PageNo { get; set; }
    /// <summary>
    /// 页码容量
    /// </summary>
    int PageSize { get; set; }
}