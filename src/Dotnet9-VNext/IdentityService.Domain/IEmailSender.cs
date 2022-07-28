namespace IdentityService.Domain;

public interface IEmailSender
{
    Task SendAsync(string toEmail, string subject, string body);
}