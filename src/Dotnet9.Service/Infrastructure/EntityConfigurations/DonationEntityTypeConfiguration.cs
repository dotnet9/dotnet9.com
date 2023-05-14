namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class DonationEntityTypeConfiguration : IEntityTypeConfiguration<Donation>
{
    public void Configure(EntityTypeBuilder<Donation> builder)
    {
        builder.ToTable("Donation");
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Content).IsRequired().HasMaxLength(DonationConsts.MaxContentLength);
    }
}