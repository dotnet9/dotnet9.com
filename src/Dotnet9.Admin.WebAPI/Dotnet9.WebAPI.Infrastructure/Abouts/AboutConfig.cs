namespace Dotnet9.WebAPI.Infrastructure.Abouts;

internal class AboutConfig : IEntityTypeConfiguration<About>
{
    public void Configure(EntityTypeBuilder<About> builder)
    {
        builder.ToTable($"{Dotnet9Consts.DbTablePrefix}Abouts", Dotnet9Consts.DbSchema);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(AboutConsts.MaxContentLength);
    }
}