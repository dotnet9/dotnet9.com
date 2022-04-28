using AutoMapper;
using Dotnet9.Application.Contracts.Users;
using Dotnet9.Core;
using Dotnet9.Domain.Users;

namespace Dotnet9.Application.Users;

public class UserAppService : IUserAppService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserAppService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<bool> ExistAdminAccountAsync()
    {
        return await _userRepository.GetAsync(x => x.Id > 0) != null;
    }

    public async Task<bool> CreateAdminAccountAsync(UserForCreationDto request)
    {
        var userForDb = _mapper.Map<UserForCreationDto, AdminAccountForCreation>(request);
        return await _userRepository.InitAccountAsync(userForDb) > 0;
    }

    public async Task<LoginResultDto> LoginAsync(UserForLoginDto request)
    {
        var account = await _userRepository.GetAsync(x => x.Account == request.Account);
        if (account == null) return LoginResultDto.Fail("不存在的账号");
        if (account.Disable) return LoginResultDto.Fail("账号已经被禁用");
        if (account.LockedDate > DateTimeOffset.Now) return LoginResultDto.Fail("账号已经被锁定");
        if (!IdentitySecurity.VerifyHashedPassword(account.Password!, request.Password))
        {
            await _userRepository.LoginFailAsync(account);
            return LoginResultDto.Fail("账号或密码错误");
        }

        await _userRepository.LoginSuccessAsync(account);
        return LoginResultDto.Success();
    }
}