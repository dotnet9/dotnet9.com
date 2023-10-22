namespace SqlSugar;

public class PageResult<T>
{
    /// <summary>
    /// 当前页
    /// </summary>
    public int PageNo { get; set; }
    /// <summary>
    /// 页容量
    /// </summary>
    public int PageSize { get; set; }
    /// <summary>
    /// 总页数
    /// </summary>
    public int Pages { get; set; }
    /// <summary>
    /// 总条数
    /// </summary>
    public int Total { get; set; }
    /// <summary>
    /// 数据
    /// </summary>
    public IList<T> Rows { get; set; }
}