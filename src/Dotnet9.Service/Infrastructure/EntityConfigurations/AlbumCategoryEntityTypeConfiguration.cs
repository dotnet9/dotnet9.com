namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class AlbumCategoryEntityTypeConfiguration : IEntityTypeConfiguration<AlbumCategory>
{
    public void Configure(EntityTypeBuilder<AlbumCategory> builder)
    {
        builder.ToTable("AlbumCategory");
        builder.HasKey(x => new { x.AlbumId, x.CategoryId });
        builder.HasOne<Album>().WithMany(x => x.Categories).HasForeignKey(x => x.AlbumId).IsRequired();
        builder.HasOne<Category>().WithMany().HasForeignKey(x => x.CategoryId).IsRequired();
        builder.HasIndex(x => new { x.AlbumId, x.CategoryId });
    }
}