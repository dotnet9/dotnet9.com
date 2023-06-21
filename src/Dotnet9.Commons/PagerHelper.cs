namespace Dotnet9.Commons;

public class PagerHelper
{
    public static List<int>? CalcPages(int totalPage, int showCount, int current)
    {
        var pages = new List<int>();
        if (totalPage <= showCount)
        {
            for (var i = 0; i < totalPage; i++)
            {
                pages.Add(i + 1);
            }
        }
        else if (current <= (showCount / 2 + 1))
        {
            var showPageCount = totalPage > showCount ? showCount : totalPage;
            for (int i = 0; i < showPageCount; i++)
            {
                pages.Add(i + 1);
            }
        }
        else if (current >= totalPage - (showCount / 2))
        {
            var showPageCount = totalPage > showCount ? showCount : totalPage;
            for (var i = 0; i < showPageCount; i++)
            {
                pages.Add(totalPage - showCount + 1 + i);
            }
        }
        else
        {
            var start = current - (showCount / 2);
            var end = current + (showCount / 2);
            for (; start <= end; start++)
            {
                pages.Add(start);
            }
        }

        return pages;
    }
}