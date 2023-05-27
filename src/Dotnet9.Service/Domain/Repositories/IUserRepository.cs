namespace Dotnet9.Service.Domain.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
    Task<User?> FindByIdAsync(Guid id);
    Task<User?> FindByAccountAsync(string account);
    Task<User?> FindByNickNameAsync(string nickName);
    Task<User?> FindByEmailAsync(string email);
    Task<User?> FindByPhoneNumberAsync(string phoneNumber);
}