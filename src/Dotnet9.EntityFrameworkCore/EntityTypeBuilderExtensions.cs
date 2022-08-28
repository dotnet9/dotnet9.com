using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dotnet9.EntityFrameworkCore;

public static class EntityTypeBuilderExtensions
{
    public static void ConfigureByConvention(this EntityTypeBuilder b)
    {
        _ = b.Property(nameof(EntityBase.Id)).IsRequired().HasColumnName(nameof(EntityBase.Id));
        _ = b.Property(nameof(EntityBase.CreateUserId)).HasColumnName(nameof(EntityBase.CreateUserId));
        _ = b.Property(nameof(EntityBase.CreateDate)).HasColumnName(nameof(EntityBase.CreateDate));
        _ = b.Property(nameof(EntityBase.UpdateUserId)).HasColumnName(nameof(EntityBase.UpdateUserId));
        _ = b.Property(nameof(EntityBase.UpdateDate)).HasColumnName(nameof(EntityBase.UpdateDate));
        _ = b.Property(nameof(EntityBase.IsDeleted)).IsRequired().HasColumnName(nameof(EntityBase.IsDeleted));
        _ = b.Property(nameof(EntityBase.DeleteUserId)).HasColumnName(nameof(EntityBase.DeleteUserId));
        _ = b.Property(nameof(EntityBase.DeleteDate)).HasColumnName(nameof(EntityBase.DeleteDate));
    }
}