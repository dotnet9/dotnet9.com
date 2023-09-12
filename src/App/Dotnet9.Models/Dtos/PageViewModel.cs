namespace Dotnet9.Models.Dtos;

public class PageViewModel
{
    private readonly Func<int, string> _func;

    public PageViewModel(int total, int pageSize, int currIndex, Func<int, string> func)
    {
        Total = total;
        PageSize = pageSize;
        CurrIndex = currIndex;
        _func = func;
    }

    private int Total { get; }


    private int PageSize { get; }

    private int CurrIndex { get; }

    public List<PageItem> GetUrls()
    {
        List<PageItem> list = new List<PageItem>();
        int maxPage = Total % PageSize == 0 ? Total / PageSize : (Total / PageSize) + 1;
        if (maxPage <= 0)
        {
            return list;
        }

        if (CurrIndex > 1)
        {
            list.Add(new PageItem
            {
                Text = "上一页",
                Url = _func.Invoke(CurrIndex - 1)
            });
        }


        for (int i = CurrIndex - 3; i < CurrIndex; i++)
        {
            if (i <= 0)
            {
                continue;
            }

            string url = _func.Invoke(i);
            list.Add(new PageItem
            {
                Url = url,
                Text = i.ToString()
            });
        }

        list.Add(new PageItem
        {
            Url = _func.Invoke(CurrIndex),
            IsActive = true,
            Text = CurrIndex.ToString()
        });

        for (int i = CurrIndex + 1; i < CurrIndex + 1 + 3; i++)
        {
            if (i > maxPage)
            {
                break;
            }

            list.Add(new PageItem
            {
                Url = _func.Invoke(i),
                Text = i.ToString()
            });
        }

        if (list.Count <= 1)
        {
            return list;
        }

        list.Add(new PageItem
        {
            Url = _func.Invoke(CurrIndex + 1),
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