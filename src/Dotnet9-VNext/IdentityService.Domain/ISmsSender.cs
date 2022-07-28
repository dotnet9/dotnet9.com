namespace IdentityService.Domain;

public interface ISmsSender
{
    Task SendAsync(string toPhoneNumber, params string[] args);
}