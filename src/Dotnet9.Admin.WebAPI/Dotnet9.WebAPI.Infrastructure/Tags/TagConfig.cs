namespace Dotnet9.WebAPI.Infrastructure.Tags;

internal class TagConfig : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable($"{Dotnet9Consts.DbTablePrefix}Tags", Dotnet9Consts.DbSchema);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(TagConsts.MaxNameLength);
        builder.HasIndex(x => x.Name);
    }
}