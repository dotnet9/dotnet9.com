namespace Dotnet9.WebAPI.Infrastructure.Privacies;

internal class PrivacyConfig : IEntityTypeConfiguration<Privacy>
{
    public void Configure(EntityTypeBuilder<Privacy> builder)
    {
        builder.ToTable($"{Dotnet9Consts.DbTablePrefix}Privacies", Dotnet9Consts.DbSchema);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(PrivacyConsts.MaxContentLength);
    }
}