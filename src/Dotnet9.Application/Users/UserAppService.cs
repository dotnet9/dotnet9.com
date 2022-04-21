using AutoMapper;
using Dotnet9.Application.Contracts.Users;
using Dotnet9.Domain.Users;

namespace Dotnet9.Application.Users;

public class UserAppService : IUserAppService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

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
}