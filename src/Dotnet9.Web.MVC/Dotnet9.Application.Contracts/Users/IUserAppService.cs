namespace Dotnet9.Application.Contracts.Users;

public interface IUserAppService
{
    Task<bool> ExistAdminAccountAsync();

    Task<bool> CreateAdminAccountAsync(UserForCreationDto request);

    Task<LoginResultDto> LoginAsync(UserForLoginDto request);
}