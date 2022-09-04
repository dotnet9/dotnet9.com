namespace Dotnet9.WebAPI.Events;

public record UserCreatedEvent(Guid Id, string UserName, string Password, string PhoneNumber);