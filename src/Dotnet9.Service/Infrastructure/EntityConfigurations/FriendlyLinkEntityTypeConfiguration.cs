namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class FriendlyLinkEntityTypeConfiguration : IEntityTypeConfiguration<FriendlyLink>
{
    public void Configure(EntityTypeBuilder<FriendlyLink> builder)
    {
        builder.ToTable("FriendlyLink");

        builder.Property(friendlyLink => friendlyLink.Id).IsRequired();

        builder.Property(friendlyLink => friendlyLink.Index).IsRequired();

        builder.Property(friendlyLink => friendlyLink.Name).IsRequired().HasMaxLength(FriendlyLinkConsts.MaxNameLength);

        builder.Property(friendlyLink => friendlyLink.Url).IsRequired().HasMaxLength(FriendlyLinkConsts.MaxUrlLength);

        builder.Property(friendlyLink => friendlyLink.Description).IsRequired()
            .HasMaxLength(FriendlyLinkConsts.MaxDescriptionLength);
    }
}