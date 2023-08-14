namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class BlogCountEntityTypeConfiguration : IEntityTypeConfiguration<BlogCount>
{
    public void Configure(EntityTypeBuilder<BlogCount> builder)
    {
        builder.ToTable("BlogCount");
        builder.Property(x => x.BlogId).IsRequired();
        builder.Property(x => x.Ip).IsRequired().HasMaxLength(BlogCountConsts.MaxIpLength);
        builder.Property(x => x.Kind).IsRequired();
        builder.Property(x => x.CreationTime).IsRequired();
        builder.HasIndex(x => x.BlogId); //.HasMethod("GIN");
        builder.HasIndex(x => x.Ip); //.HasMethod("GIN");
        builder.HasIndex(x => x.Kind); //.HasMethod("GIN");
        builder.HasKey(x => new { x.BlogId, x.Ip, x.Kind });
        builder.HasOne<Blog>().WithMany(x => x.Counts).HasForeignKey(x => x.BlogId).IsRequired();
        builder.HasIndex(x => new { x.BlogId, x.Ip, x.Kind });
    }
}