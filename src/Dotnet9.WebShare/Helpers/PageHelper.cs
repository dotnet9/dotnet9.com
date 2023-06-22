namespace Dotnet9.WebShare.Helpers;

public static class PageHelper
{
    public const int MinPageCurrent = 1;
    public const int MinPageSize = 8;
    public const int MaxPageSize = 30;

    public static (int PageSize, int Current) CheckPage(int pageSize, int current)
    {
        if (current <= 0)
        {
            current = MinPageCurrent;
        }

        if (pageSize < MinPageSize)
        {
            pageSize = MinPageSize;
        }
        else if (pageSize > MaxPageSize)
        {
            pageSize = MaxPageSize;
        }

        return (pageSize, current);
    }
}