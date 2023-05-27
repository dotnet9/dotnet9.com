namespace Dotnet9.Service.Domain.Aggregates.Systems;

public static class UserConsts
{
    public const int MaxAccountLength = 32;
    public const int MinAccountLength = 2;
    public const int MaxNickNameLength = 32;
    public const int MinNickNameLength = 2;
    public const int MaxEmailLength = 32;
    public const int MinEmailLength = 5;
    public const int MaxPhoneNumberLength = 32;
    public const int MinPhoneNumberLength = 11;
    public const int MaxPasswordLength = 32;
    public const int MinPasswordLength = 5;
    public const int MaxLoginFailTime = 5;
    public const int MaxLockedMinutes = 5;
}