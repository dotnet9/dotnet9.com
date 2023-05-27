namespace Dotnet9.Service.Domain.Aggregates.Systems;

public class UserManager : IScopedDependency
{
    private readonly IUserRepository _repository;

    public UserManager(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> CreateAsync(Guid? id, string account, string nickName, string phoneNumber, string email)
    {
        var isNew = id == null;
        User? old = null;
        if (isNew)
        {
            id = Guid.NewGuid();
        }
        else
        {
            old = await _repository.FindByIdAsync(id!.Value);
            if (old == null)
            {
                throw new Exception($"不存在的用户: {id}");
            }
        }

        Check.NotNullOrWhiteSpace(account, nameof(account), UserConsts.MaxAccountLength, UserConsts.MinAccountLength);
        Check.NotNullOrWhiteSpace(nickName, nameof(nickName), UserConsts.MaxNickNameLength,
            UserConsts.MinNickNameLength);
        Check.NotNullOrWhiteSpace(phoneNumber, nameof(phoneNumber), UserConsts.MaxPhoneNumberLength,
            UserConsts.MinPhoneNumberLength);
        Check.NotNullOrWhiteSpace(email, nameof(email), UserConsts.MaxEmailLength, UserConsts.MinEmailLength);


        var exist = await _repository.FindByAccountAsync(account);
        if ((isNew && exist != null) ||
            (isNew == false && exist != null && exist.Id != old!.Id))
        {
            throw new Exception($"存在同账号的用户: {account}");
        }

        exist = await _repository.FindByNickNameAsync(nickName);
        if ((isNew && exist != null) ||
            (isNew == false && exist != null && exist.Id != old!.Id))
        {
            throw new Exception($"存在同昵称的用户: {nickName}");
        }

        exist = await _repository.FindByPhoneNumberAsync(phoneNumber);
        if ((isNew && exist != null) ||
            (isNew == false && exist != null && exist.Id != old!.Id))
        {
            throw new Exception($"存在同电话号码的用户: {phoneNumber}");
        }

        exist = await _repository.FindByEmailAsync(email);
        if ((isNew && exist != null) ||
            (isNew == false && exist != null && exist.Id != old!.Id))
        {
            throw new Exception($"存在同邮箱的用户: {email}");
        }

        if (isNew)
        {
            old = new User(id.Value, account, nickName, phoneNumber, email);
        }
        else
        {
            old!.ChangeAccount(account)
                .ChangeNickName(nickName)
                .ChangePhoneNumber(phoneNumber)
                .ChangeEmail(email);
        }

        return old;
    }

    public User CreateForSeed(string account, string nickName, string phoneNumber, string email)
    {
        return new User(Guid.NewGuid(), account, nickName, phoneNumber, email);
    }
}