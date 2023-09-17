namespace Dotnet9.Commons;

public static class PaginationHelper
{
    public static int GetPageCount(this int total, int pageSize)
    {
        var pageCount = total / pageSize;
        if (total % pageSize > 0)
        {
            pageCount++;
        }

        return pageCount;
    }

    /// <summary>
    /// 生成页码
    /// </summary>
    /// <param name="total">总条数</param>
    /// <param name="pageSize">单页大小</param>
    /// <param name="current">当前页标</param>
    /// <param name="length">取前后多少页</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static int[] GetPages(this int total, int pageSize, int current, int length)
    {
        var pageCount = GetPageCount(total, pageSize);

        if (current < 1)
        {
            current = 1;
        }

        if (current > pageCount)
        {
            current = pageCount;
        }

        var pages = new List<int>();

        var len = length;
        if (len % 2 == 0)
        {
            len++;
        }

        var half = len / 2;

        var start = current - half;
        var end = current + half;

        if (start < 1)
        {
            var p = 1 - start;
            start += p;
            end += p;
        }

        if (end > pageCount)
        {
            var p = end - pageCount;
            end = pageCount;
            if (start - p >= 1)
            {
                start -= p;
            }
            else
            {
                start = 1;
            }
        }

        for (int i = start, j = 0; i <= end && j < length; i++, j++)
        {
            pages.Add(i);
        }

        return pages.ToArray();
    }
}