namespace Dotnet9.WebAPI.Infrastructure.Donations;

internal class DonationConfig : IEntityTypeConfiguration<Donation>
{
    public void Configure(EntityTypeBuilder<Donation> builder)
    {
        builder.ToTable($"{Dotnet9Consts.DbTablePrefix}Donations", Dotnet9Consts.DbSchema);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(DonationConsts.MaxContentLength);
    }
}