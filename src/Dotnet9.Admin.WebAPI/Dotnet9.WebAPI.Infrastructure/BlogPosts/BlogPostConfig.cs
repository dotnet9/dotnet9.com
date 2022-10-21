namespace Dotnet9.WebAPI.Infrastructure.BlogPosts;

internal class BlogPostConfig : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.ToTable($"{Dotnet9Consts.DbTablePrefix}BlogPosts", Dotnet9Consts.DbSchema);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(BlogPostConsts.MaxTitleLength);
        builder.Property(x => x.Slug).IsRequired().HasMaxLength(BlogPostConsts.MaxSlugLength);
        builder.Property(x => x.Description).HasMaxLength(BlogPostConsts.MaxDescriptionLength);
        builder.Property(x => x.Cover).HasMaxLength(BlogPostConsts.MaxCoverLength);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(BlogPostConsts.MaxContentLength);
        builder.Property(x => x.CopyrightType);
        builder.Property(x => x.Original).HasMaxLength(BlogPostConsts.MaxOriginalLength);
        builder.Property(x => x.OriginalAvatar).HasMaxLength(BlogPostConsts.MaxOriginalAvatarLength);
        builder.Property(x => x.OriginalTitle).HasMaxLength(BlogPostConsts.MaxOriginalTitleLength);
        builder.Property(x => x.OriginalLink).HasMaxLength(BlogPostConsts.MaxOriginalLinkLength);
        builder.Property(x => x.Banner);
        builder.Property(x => x.Visible);
        builder.Property(x => x.ViewCount);
        // TODO GIN索引异常，暂时注释
        builder.HasIndex(x => x.Title); //.HasMethod("GIN");
        builder.HasIndex(x => x.Slug); //.HasMethod("GIN");
        builder.HasIndex(x => x.Original); //.HasMethod("GIN");
        builder.HasIndex(x => x.OriginalTitle); //.HasMethod("GIN");
        builder.HasIndex(x => x.Description); //.HasMethod("GIN");
        //builder.HasIndex(x => x.Content); //.HasMethod("GIN");
        builder.HasMany(x => x.Albums).WithOne().HasForeignKey(x => x.BlogPostId).IsRequired();
        builder.HasMany(x => x.Categories).WithOne().HasForeignKey(x => x.BlogPostId).IsRequired();
        builder.HasMany(x => x.Tags).WithOne().HasForeignKey(x => x.BlogPostId).IsRequired();
    }
}