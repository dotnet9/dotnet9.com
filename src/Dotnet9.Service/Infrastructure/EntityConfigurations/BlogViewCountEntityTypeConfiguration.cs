namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class BlogViewCountEntityTypeConfiguration : IEntityTypeConfiguration<BlogViewCount>
{
    public void Configure(EntityTypeBuilder<BlogViewCount> builder)
    {
        builder.ToTable("BlogViewCount");
        builder.Property(x => x.Slug).IsRequired().HasMaxLength(BlogConsts.MaxSlugLength);
        builder.Property(x => x.Ip).IsRequired().HasMaxLength(BlogViewCountConsts.MaxIpLength);
        builder.Property(x => x.CreationTime).IsRequired();
        builder.HasIndex(x => x.Slug); //.HasMethod("GIN");
        builder.HasIndex(x => x.Ip); //.HasMethod("GIN");
    }
}