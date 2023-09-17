namespace Dotnet9.WebAPI.Infrastructure.BlogPosts;

internal class BlogPostTagConfig : IEntityTypeConfiguration<BlogPostTag>
{
    public void Configure(EntityTypeBuilder<BlogPostTag> builder)
    {
        builder.ToTable($"{Dotnet9Consts.DbTablePrefix}BlogPostTags", Dotnet9Consts.DbSchema);
        builder.HasKey(x => new { x.BlogPostId, x.TagId });
        builder.HasOne<BlogPost>().WithMany(x => x.Tags).HasForeignKey(x => x.BlogPostId).IsRequired();
        builder.HasOne<Tag>().WithMany().HasForeignKey(x => x.TagId).IsRequired();
        builder.HasIndex(x => new { x.BlogPostId, x.TagId });
    }
}