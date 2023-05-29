namespace Dotnet9.Contracts.Dto.Users;

public record UserDto(Guid Id, string Account, string NickName, string PhoneNumber, string Email, string Token);