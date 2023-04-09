namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class BlogCategoryEntityTypeConfiguration : IEntityTypeConfiguration<BlogCategory>
{
    public void Configure(EntityTypeBuilder<BlogCategory> builder)
    {
        builder.ToTable("BlogCategory");
        builder.HasKey(x => new { x.BlogId, x.CategoryId });
        builder.HasOne<Blog>().WithMany(x => x.Categories).HasForeignKey(x => x.BlogId).IsRequired();
        builder.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId).IsRequired();
        builder.HasIndex(x => new { x.BlogId, x.CategoryId });
    }
}