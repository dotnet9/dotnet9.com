namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class BlogSearchCountEntityTypeConfiguration : IEntityTypeConfiguration<BlogSearchCount>
{
    public void Configure(EntityTypeBuilder<BlogSearchCount> builder)
    {
        builder.ToTable("BlogSearchCount");
        builder.Property(x => x.Keywords).IsRequired().HasMaxLength(BlogConsts.MaxSlugLength);
        builder.Property(x => x.Ip).IsRequired().HasMaxLength(BlogViewCountConsts.MaxIpLength);
        builder.Property(x => x.CreationTime).IsRequired();
        builder.HasIndex(x => x.Keywords); //.HasMethod("GIN");
        builder.HasIndex(x => x.Ip); //.HasMethod("GIN");
    }
}