namespace Dotnet9.Service.Infrastructure.EFCore;

public static class EFCoreExtensions
{
    public static IQueryable<T> Query<T>(this DbContext ctx) where T : class, IEntity
    {
        return ctx.Set<T>().AsNoTracking();
    }
}