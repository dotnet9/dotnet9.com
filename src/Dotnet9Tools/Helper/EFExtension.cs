using System.Linq.Expressions;

namespace Dotnet9Tools.Helper;

public static class EFExtension
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> exp)
        where T : class
    {
        if (condition)
        {
            query = query.Where(exp);
        }

        return query;
    }

    /// <summary>
    ///     分页
    /// </summary>
    /// <param name="query"></param>
    /// <param name="index"></param>
    /// <param name="size"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static IQueryable<T> Page<T>(this IQueryable<T> query, int index, int size) where T : class
    {
        int skip = (index - 1) * size;
        return query.Skip(skip).Take(size);
    }
}