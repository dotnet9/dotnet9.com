namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class BlogAlbumEntityTypeConfiguration : IEntityTypeConfiguration<BlogAlbum>
{
    public void Configure(EntityTypeBuilder<BlogAlbum> builder)
    {
        builder.ToTable("BlogAlbum");
        builder.HasKey(x => new { x.BlogId, x.AlbumId });
        builder.HasOne<Blog>().WithMany(x => x.Albums).HasForeignKey(x => x.BlogId).IsRequired();
        builder.HasOne<Album>().WithMany().HasForeignKey(x => x.AlbumId).IsRequired();
        builder.HasIndex(x => new { x.BlogId, x.AlbumId });
    }
}