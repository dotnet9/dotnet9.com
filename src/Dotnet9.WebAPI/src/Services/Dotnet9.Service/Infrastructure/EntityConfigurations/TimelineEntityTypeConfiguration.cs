namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class TimelineEntityTypeConfiguration : IEntityTypeConfiguration<Timeline>
{
    public void Configure(EntityTypeBuilder<Timeline> builder)
    {
        builder.ToTable("Timeline");

        builder.Property(friendlyLink => friendlyLink.Id).IsRequired();

        builder.Property(x => x.Time);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(TimelineConsts.MaxTitleLength);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(TimelineConsts.MaxContentLength);
    }
}