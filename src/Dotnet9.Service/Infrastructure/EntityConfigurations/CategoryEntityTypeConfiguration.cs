namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Category");
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.SequenceNumber);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(CategoryConsts.MaxNameLength);
        builder.Property(x => x.Slug).IsRequired().HasMaxLength(CategoryConsts.MaxSlugLength);
        builder.Property(x => x.Cover).IsRequired().HasMaxLength(CategoryConsts.MaxCoverLength);
        builder.Property(x => x.Description).HasMaxLength(CategoryConsts.MaxDescriptionLength);
        builder.Property(x => x.ParentId);
        builder.Property(x => x.Visible);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Slug);
    }
}