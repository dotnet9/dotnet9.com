namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("Tag");
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(TagConsts.MaxNameLength);
        builder.HasIndex(x => x.Name);
    }
}