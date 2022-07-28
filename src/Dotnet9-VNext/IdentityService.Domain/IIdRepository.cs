namespace IdentityService.Domain;

public interface IIdRepository
{
    Task<User?> FindByIdAsync(Guid userId);
    Task<User?> FindByNameAsync(string userName);
    Task<User?> FindByPhoneNumberAsync(string phoneNumber);
    Task<IdentityResult> CreateAsync(User user, string password);
    Task<IdentityResult> AccessFailedAsync(User user);
    Task<string> GenerateChangePhoneNumberTokenAsync(User user, string phoneNumber);
    Task<SignInResult> ChangePhoneNumberAsync(User user, string phoneNumber, string token);
    Task<IdentityResult> ChangePasswordAsync(Guid userId, string password);
    Task<IList<string>> GetRolesAsync(User user);
    Task<IdentityResult> AddToRoleAsync(User user, string role);
    Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure);
    Task ConfirmPhoneNumberAsync(Guid id);
    public Task UpdatePhoneNumber(Guid id, string phoneNumber);
    Task<IdentityResult> RemoveUserAsync(Guid id);
    Task<(IdentityResult, User?, string? password)> AddAdminUserAsync(string userName, string phoneNumber);
    Task<(IdentityResult, User?, string? password)> ResetPasswordAsync(Guid id);
}