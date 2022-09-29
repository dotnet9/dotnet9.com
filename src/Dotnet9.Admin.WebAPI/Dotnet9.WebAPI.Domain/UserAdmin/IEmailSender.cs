namespace Dotnet9.WebAPI.Domain.UserAdmin;

public interface IEmailSender
{
    public Task SendAsync(string toEmail, string subject, string body);
}