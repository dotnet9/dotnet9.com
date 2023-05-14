namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class ActionLogEntityTypeConfiguration : IEntityTypeConfiguration<ActionLog>
{
    public void Configure(EntityTypeBuilder<ActionLog> builder)
    {
        builder.ToTable("ActionLog");

        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.UId).HasMaxLength(ActionLogConsts.MaxUIdLength);
        builder.Property(x => x.Ua).HasMaxLength(ActionLogConsts.MaxUALength);
        builder.Property(x => x.Os).HasMaxLength(ActionLogConsts.MaxOSLength);
        builder.Property(x => x.Browser).HasMaxLength(ActionLogConsts.MaxBrowserLength);
        builder.Property(x => x.Referer).HasMaxLength(ActionLogConsts.MaxRefererLength);
        builder.Property(x => x.AccessName).HasMaxLength(ActionLogConsts.MaxAccessName);
        builder.Property(x => x.Original).HasMaxLength(ActionLogConsts.MaxOriginalLength);
        builder.Property(x => x.Ip).HasMaxLength(ActionLogConsts.MaxIPLength);
        builder.Property(x => x.Url).HasMaxLength(ActionLogConsts.MaxUrlLength);
        builder.Property(x => x.Controller).HasMaxLength(ActionLogConsts.MaxControllerLength);
        builder.Property(x => x.Action).HasMaxLength(ActionLogConsts.MaxActionLength);
        builder.Property(x => x.Method).HasMaxLength(ActionLogConsts.MaxMethodLength);
        builder.Property(x => x.Arguments).HasMaxLength(ActionLogConsts.MaxArgumentsLength);
        builder.Property(x => x.Duration);
    }
}