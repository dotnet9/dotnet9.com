namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class AboutEntityTypeConfiguration : IEntityTypeConfiguration<About>
{
    public void Configure(EntityTypeBuilder<About> builder)
    {
        builder.ToTable("About");

        builder.Property(about => about.Id).IsRequired();

        builder.Property(about => about.Content).IsRequired().HasMaxLength(AboutConsts.MaxContentLength);
    }
}