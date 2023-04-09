namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class AlbumEntityTypeConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.ToTable("Album");
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.SequenceNumber);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(AlbumConsts.MaxNameLength);
        builder.Property(x => x.Slug).IsRequired().HasMaxLength(AlbumConsts.MaxSlugLength);
        builder.Property(x => x.Cover).IsRequired().HasMaxLength(AlbumConsts.MaxCoverLength);
        builder.Property(x => x.Description).HasMaxLength(AlbumConsts.MaxDescriptionLength);
        builder.Property(x => x.Visible);
        builder.HasIndex(x => x.Name);
        builder.HasIndex(x => x.Slug);
    }
}