namespace Dotnet9.WebAPI.Infrastructure.ActionLogs;

internal class ActionLogConfig : IEntityTypeConfiguration<ActionLog>
{
    public void Configure(EntityTypeBuilder<ActionLog> builder)
    {
        builder.ToTable($"{Dotnet9Consts.DbTablePrefix}ActionLogs", Dotnet9Consts.DbSchema);
        builder.Property(x => x.UId).HasMaxLength(ActionLogConsts.MaxUIdLength);
        builder.Property(x => x.UA).HasMaxLength(ActionLogConsts.MaxUALength);
        builder.Property(x => x.OS).HasMaxLength(ActionLogConsts.MaxOSLength);
        builder.Property(x => x.Browser).HasMaxLength(ActionLogConsts.MaxBrowserLength);
        builder.Property(x => x.Referer).HasMaxLength(ActionLogConsts.MaxRefererLength);
        builder.Property(x => x.AccessName).HasMaxLength(ActionLogConsts.MaxAccessName);
        builder.Property(x => x.Original).HasMaxLength(ActionLogConsts.MaxOriginalLength);
        builder.Property(x => x.IP).HasMaxLength(ActionLogConsts.MaxIPLength);
        builder.Property(x => x.Url).HasMaxLength(ActionLogConsts.MaxUrlLength);
        builder.Property(x => x.Controller).HasMaxLength(ActionLogConsts.MaxControllerLength);
        builder.Property(x => x.Action).HasMaxLength(ActionLogConsts.MaxActionLength);
        builder.Property(x => x.Method).HasMaxLength(ActionLogConsts.MaxMethodLength);
        builder.Property(x => x.Arguments).HasMaxLength(ActionLogConsts.MaxArgumentsLength);
        builder.Property(x => x.Duration);
    }
}