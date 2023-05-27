namespace Dotnet9.Service.Infrastructure.Repositories;

public class UserRepository : Repository<Dotnet9DbContext, User, Guid>, IUserRepository
{
    public UserRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork)
        : base(context, unitOfWork)
    {
    }

    public Task<User?> FindByIdAsync(Guid id)
    {
        return Context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<User?> FindByAccountAsync(string account)
    {
        return Context.Users.FirstOrDefaultAsync(x => x.Account == account);
    }

    public Task<User?> FindByNickNameAsync(string nickName)
    {
        return Context.Users.FirstOrDefaultAsync(x => x.NickName == nickName);
    }

    public Task<User?> FindByEmailAsync(string email)
    {
        return Context.Users.FirstOrDefaultAsync(x => x.Email == email);
    }

    public Task<User?> FindByPhoneNumberAsync(string phoneNumber)
    {
        return Context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber);
    }
}