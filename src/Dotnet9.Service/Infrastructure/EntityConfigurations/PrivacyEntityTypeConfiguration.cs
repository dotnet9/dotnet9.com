namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class PrivacyEntityTypeConfiguration : IEntityTypeConfiguration<Privacy>
{
    public void Configure(EntityTypeBuilder<Privacy> builder)
    {
        builder.ToTable("Privacy");
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Content).IsRequired().HasMaxLength(PrivacyConsts.MaxContentLength);
    }
}