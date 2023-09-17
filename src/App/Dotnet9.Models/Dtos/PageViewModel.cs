namespace Dotnet9.Models.Dtos;

public class PageViewModel
{
    private readonly Func<int, string> _getNewUrlfunc;

    public PageViewModel(int total, int pageSize, int pageIndex, Func<int, string> getNewUrlFunc)
    {
        Total = total;
        PageSize = pageSize;
        PageIndex = pageIndex;
        _getNewUrlfunc = getNewUrlFunc;
    }

    private int Total { get; }


    private int PageSize { get; }

    private int PageIndex { get; }

    public List<PageItem> GetUrls()
    {
        List<PageItem> list = new List<PageItem>();
        int maxPage = Total % PageSize == 0 ? Total / PageSize : (Total / PageSize) + 1;
        if (maxPage <= 0)
        {
            return list;
        }

        if (PageIndex > 1)
        {
            list.Add(new PageItem
            {
                Text = "上一页",
                Url = _getNewUrlfunc.Invoke(PageIndex - 1)
            });
        }


        for (int i = PageIndex - 3; i < PageIndex; i++)
        {
            if (i <= 0)
            {
                continue;
            }

            string url = _getNewUrlfunc.Invoke(i);
            list.Add(new PageItem
            {
                Url = url,
                Text = i.ToString()
            });
        }

        list.Add(new PageItem
        {
            Url = _getNewUrlfunc.Invoke(PageIndex),
            IsActive = true,
            Text = PageIndex.ToString()
        });

        for (int i = PageIndex + 1; i < PageIndex + 1 + 3; i++)
        {
            if (i > maxPage)
            {
                break;
            }

            list.Add(new PageItem
            {
                Url = _getNewUrlfunc.Invoke(i),
                Text = i.ToString()
            });
        }

        if (list.Count <= 1)
        {
            return list;
        }

        list.Add(new PageItem
        {
            Url = _getNewUrlfunc.Invoke(PageIndex + 1),
            Text = "下一页"
        });

        return list;
    }
}

public class PageItem
{
    public string Url { get; set; }
    public string Text { get; set; }
    public bool IsActive { get; set; }
}