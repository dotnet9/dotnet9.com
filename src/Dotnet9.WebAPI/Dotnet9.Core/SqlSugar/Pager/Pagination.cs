namespace SqlSugar;
/// <summary>
/// 分页查询基类
/// </summary>
public class Pagination : IPagination
{
    /// <summary>
    /// 当前页码
    /// </summary>
    public int PageNo { get; set; } = 1;

    /// <summary>
    /// 页码容量
    /// </summary>
    public int PageSize { get; set; } = 10;
}