namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class BlogEntityTypeConfiguration : IEntityTypeConfiguration<Blog>
{
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.ToTable("Blog");
        builder.Property(x => x.Title).IsRequired().HasMaxLength(BlogConsts.MaxTitleLength);
        builder.Property(x => x.Slug).IsRequired().HasMaxLength(BlogConsts.MaxSlugLength);
        builder.Property(x => x.Description).HasMaxLength(BlogConsts.MaxDescriptionLength);
        builder.Property(x => x.Cover).HasMaxLength(BlogConsts.MaxCoverLength);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(BlogConsts.MaxContentLength);
        builder.Property(x => x.CopyrightType);
        builder.Property(x => x.Original).HasMaxLength(BlogConsts.MaxOriginalLength);
        builder.Property(x => x.OriginalAvatar).HasMaxLength(BlogConsts.MaxOriginalAvatarLength);
        builder.Property(x => x.OriginalTitle).HasMaxLength(BlogConsts.MaxOriginalTitleLength);
        builder.Property(x => x.OriginalLink).HasMaxLength(BlogConsts.MaxOriginalLinkLength);
        builder.Property(x => x.Banner);
        builder.Property(x => x.Visible);
        builder.Property(x => x.ViewCount);
        builder.Property(x => x.LikeCount);
        // TODO GIN索引异常，暂时注释
        builder.HasIndex(x => x.Title); //.HasMethod("GIN");
        builder.HasIndex(x => x.Slug); //.HasMethod("GIN");
        builder.HasIndex(x => x.Original); //.HasMethod("GIN");
        builder.HasIndex(x => x.OriginalTitle); //.HasMethod("GIN");
        builder.HasIndex(x => x.Description); //.HasMethod("GIN");
        //builder.HasIndex(x => x.Content); //.HasMethod("GIN");
        builder.HasMany(x => x.Albums).WithOne().HasForeignKey(x => x.BlogId).IsRequired();
        builder.HasMany(x => x.Categories).WithOne().HasForeignKey(x => x.BlogId).IsRequired();
        builder.HasMany(x => x.Tags).WithOne().HasForeignKey(x => x.BlogId).IsRequired();
    }
}