namespace Dotnet9.WebAPI.Infrastructure.BlogPosts;

internal class BlogPostAlbumConfig : IEntityTypeConfiguration<BlogPostAlbum>
{
    public void Configure(EntityTypeBuilder<BlogPostAlbum> builder)
    {
        builder.ToTable($"{Dotnet9Consts.DbTablePrefix}BlogPostAlbums", Dotnet9Consts.DbSchema);
        builder.HasKey(x => new { x.BlogPostId, x.AlbumId });
        builder.HasOne<BlogPost>().WithMany(x => x.Albums).HasForeignKey(x => x.BlogPostId).IsRequired();
        builder.HasOne<Album>().WithMany().HasForeignKey(x => x.AlbumId).IsRequired();
        builder.HasIndex(x => new { x.BlogPostId, x.AlbumId });
    }
}