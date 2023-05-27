namespace Dotnet9.Service.Infrastructure.EntityConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.Property(x => x.Id).IsRequired();
        builder.Property(x => x.Account).IsRequired().HasMaxLength(UserConsts.MaxAccountLength);
        builder.Property(x => x.NickName).IsRequired().HasMaxLength(UserConsts.MaxNickNameLength);
        builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(UserConsts.MaxPhoneNumberLength);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(UserConsts.MaxEmailLength);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(UserConsts.MaxEmailLength);
        builder.HasIndex(x => x.LockedTime);
        builder.HasIndex(x => x.LoginFailCount);
    }
}