namespace Dotnet9.Service.Application.Auth;

public class CommandHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtOptions _jwtOptions;

    public CommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IOptions<JwtOptions> jwtOptions)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtOptions = jwtOptions.Value;
    }

    [EventHandler]
    public async Task LoginByAccountAsync(LoginByAccountCommand command)
    {
        var model = command.Model;
        var user = await _userRepository.FindByAccountAsync(model.Account);
        if (user is null)
        {
            throw new UserFriendlyException(UserFriendlyExceptionCodes.UserAccountNotExist, model.Account);
        }

        if (user.LockedTime > DateTime.Now)
        {
            throw new UserFriendlyException(UserFriendlyExceptionCodes.UserAccountIsLocked, user.Account);
        }

        if (MD5Utils.EncryptRepeat(model.Password) != user.Password)
        {
            user.IncreaseLoginFailCount();
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            throw new UserFriendlyException(UserFriendlyExceptionCodes.UserVerificationFailed, user.Account);
        }

        if (user.LoginFailCount > UserConsts.MaxLoginFailTime)
        {
            user.LockedUser();
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitAsync();
            throw new UserFriendlyException(UserFriendlyExceptionCodes.UserAccountIsLocked, user.Account);
        }

        user.ResetLoginFailCount();
        await _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangesAsync();
        await _unitOfWork.CommitAsync();

        var claimsIdentity = JwtHelper.GetClaimsIdentity(user);
        var token = JwtHelper.GeneratorAccessToken(claimsIdentity, _jwtOptions);

        command.Result = new UserDto(user.Id, user.Account, user.NickName, user.PhoneNumber, user.Email, token);
    }
}