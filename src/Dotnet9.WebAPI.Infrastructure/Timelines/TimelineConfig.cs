namespace Dotnet9.WebAPI.Infrastructure.Timelines;

internal class TimelineConfig : IEntityTypeConfiguration<Timeline>
{
    public void Configure(EntityTypeBuilder<Timeline> builder)
    {
        builder.ToTable($"{Dotnet9Consts.DbTablePrefix}Timelines", Dotnet9Consts.DbSchema);
        builder.Property(x => x.Time);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(TimelineConsts.MaxTitleLength);
        builder.Property(x => x.Content).IsRequired().HasMaxLength(TimelineConsts.MaxContentLength);
    }
}