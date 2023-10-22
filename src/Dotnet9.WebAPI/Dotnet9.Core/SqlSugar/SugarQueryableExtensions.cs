using System.Threading.Tasks;

namespace SqlSugar;
/// <summary>
/// 扩展SqlSugar SugarQueryable进行分页
/// </summary>
public static class SugarQueryableExtensions
{
    /// <summary>
    /// 分页拓展
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public static PageResult<T> ToPagedList<T>(this ISugarQueryable<T> queryable, IPagination input)
    {
        int totalCount = 0;
        var items = queryable.ToPageList(input.PageNo, input.PageSize, ref totalCount);
        var totalPages = (int)Math.Ceiling(totalCount / (double)input.PageSize);
        return new PageResult<T>()
        {
            PageNo = input.PageNo,
            PageSize = input.PageSize,
            Rows = items,
            Total = totalCount,
            Pages = totalPages,
        };
    }

    /// <summary>
    /// 分页拓展
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public static async Task<PageResult<T>> ToPagedListAsync<T>(this ISugarQueryable<T> queryable, IPagination input)
    {
        RefAsync<int> totalCount = 0;
        var items = await queryable.ToPageListAsync(input.PageNo, input.PageSize, totalCount);
        var totalPages = (int)Math.Ceiling(totalCount / (double)input.PageSize);
        return new PageResult<T>()
        {
            PageNo = input.PageNo,
            PageSize = input.PageSize,
            Rows = items,
            Total = (int)totalCount,
            Pages = totalPages,
        };
    }
}