namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class BlogTagEntityTypeConfiguration : IEntityTypeConfiguration<BlogTag>
{
    public void Configure(EntityTypeBuilder<BlogTag> builder)
    {
        builder.ToTable("BlogTag");
        builder.HasKey(x => new { x.BlogId, x.TagId });
        builder.HasOne<Blog>().WithMany(x => x.Tags).HasForeignKey(x => x.BlogId).IsRequired();
        builder.HasOne<Tag>().WithMany().HasForeignKey(x => x.TagId).IsRequired();
        builder.HasIndex(x => new { x.BlogId, x.TagId });
    }
}