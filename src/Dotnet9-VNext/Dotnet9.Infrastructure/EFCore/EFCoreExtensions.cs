// ReSharper disable once CheckNamespace

using Microsoft.EntityFrameworkCore.Metadata;

namespace Microsoft.EntityFrameworkCore;

public static class EFCoreExtensions
{
    public static void EnableSoftDeletionGlobalFilter(this ModelBuilder modelBuilder)
    {
        IEnumerable<IMutableEntityType> entityTypesHasSoftDeletion = modelBuilder.Model.GetEntityTypes()
            .Where(e => e.ClrType.IsAssignableTo(typeof(ISoftDelete)));

        foreach (IMutableEntityType entityType in entityTypesHasSoftDeletion)
        {
            IMutableProperty? isDeletedProperty = entityType.FindProperty(nameof(ISoftDelete.IsDeleted));
            ParameterExpression parameter = Expression.Parameter(entityType.ClrType, "p");
            LambdaExpression filter =
                Expression.Lambda(Expression.Not(Expression.Property(parameter, isDeletedProperty.PropertyInfo)),
                    parameter);
            entityType.SetQueryFilter(filter);
        }
    }

    public static IQueryable<T> Query<T>(this DbContext ctx) where T : class, IEntity
    {
        return ctx.Set<T>().AsNoTracking();
    }
}