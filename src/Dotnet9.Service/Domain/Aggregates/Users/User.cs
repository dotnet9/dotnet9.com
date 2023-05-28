namespace Dotnet9.Service.Domain.Aggregates.Users;

public class User : FullAggregateRoot<Guid, int>
{
    private User()
    {
    }

    internal User(Guid id, string account, string nickName, string phoneNumber, string email)
    {
        Id = id;
        ChangeAccount(account);
        ChangeNickName(nickName);
        ChangePhoneNumber(phoneNumber);
        ChangeEmail(email);
    }

    public string Account { get; private set; } = null!;
    public string NickName { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Password { get; private set; } = null!;
    public DateTime LockedTime { get; private set; }
    public int LoginFailCount { get; private set; }

    public User ChangeAccount(string account)
    {
        Account = Check.NotNullOrWhiteSpace(account, nameof(account), UserConsts.MaxAccountLength,
            UserConsts.MinAccountLength);
        return this;
    }

    public User ChangeNickName(string nickName)
    {
        NickName = Check.NotNullOrWhiteSpace(nickName, nameof(nickName), UserConsts.MaxNickNameLength,
            UserConsts.MinNickNameLength);
        return this;
    }

    public User ChangePhoneNumber(string phoneNumber)
    {
        PhoneNumber = Check.NotNullOrWhiteSpace(phoneNumber, nameof(phoneNumber), UserConsts.MaxPhoneNumberLength,
            UserConsts.MinPhoneNumberLength);
        return this;
    }

    public User ChangeEmail(string email)
    {
        Email = Check.NotNullOrWhiteSpace(email, nameof(email), UserConsts.MaxEmailLength,
            UserConsts.MinEmailLength);
        return this;
    }

    public User ResetPassword()
    {
        Password = MD5Utils.EncryptRepeat("Dotnet9");
        return this;
    }

    public User ChangePassword(string password)
    {
        Password = MD5Utils.EncryptRepeat(Check.NotNullOrWhiteSpace(password, nameof(password),
            UserConsts.MaxPasswordLength,
            UserConsts.MinPasswordLength));
        return this;
    }

    public User IncreaseLoginFailCount()
    {
        LoginFailCount++;
        return this;
    }

    public User ResetLoginFailCount()
    {
        LoginFailCount = 0;
        LockedTime = default;
        return this;
    }

    public bool IsLocked()
    {
        return LoginFailCount >= UserConsts.MaxLoginFailTime;
    }

    public bool IsCanLogin()
    {
        return LockedTime == default || LockedTime < DateTime.Now;
    }

    public User LockedUser()
    {
        LoginFailCount = 0;
        LockedTime = DateTime.Now.AddMinutes(UserConsts.MaxLockedMinutes);
        return this;
    }
}