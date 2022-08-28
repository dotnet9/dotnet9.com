namespace Dotnet9.WebAPI.Domain.UserAdmin;

public class IdDomainService
{
    private readonly IOptions<JWTOptions> _optJwt;
    private readonly IIdRepository _repository;
    private readonly ITokenService _tokenService;

    public IdDomainService(IIdRepository repository, ITokenService tokenService, IOptions<JWTOptions> optJWT)
    {
        _repository = repository;
        _tokenService = tokenService;
        _optJwt = optJWT;
    }

    private async Task<SignInResult> CheckUserNameAndPwdAsync(string userName, string password)
    {
        var user = await _repository.FindByNameAsync(userName);
        if (user == null)
        {
            return SignInResult.Failed;
        }

        //CheckPasswordSignInAsync会对于多次重复失败进行账号禁用
        var result = await _repository.CheckForSignInAsync(user, password, true);
        return result;
    }

    private async Task<SignInResult> CheckPhoneNumberAndPwdAsync(string phoneNumber, string password)
    {
        var user = await _repository.FindByPhoneNumberAsync(phoneNumber);
        if (user == null)
        {
            return SignInResult.Failed;
        }

        var result = await _repository.CheckForSignInAsync(user, password, true);
        return result;
    }

    public async Task<(SignInResult Result, string? Token)> LoginByPhoneAndPwdAsync(string phoneNumber, string password)
    {
        var checkResult = await CheckPhoneNumberAndPwdAsync(phoneNumber, password);
        if (!checkResult.Succeeded)
        {
            return (checkResult, null);
        }

        var user = await _repository.FindByPhoneNumberAsync(phoneNumber);
        var token = await BuildTokenAsync(user!);
        return (SignInResult.Success, token);
    }

    public async Task<(SignInResult Result, string? Token)> LoginByUserNameAndPwdAsync(string userName, string password)
    {
        var checkResult = await CheckUserNameAndPwdAsync(userName, password);
        if (!checkResult.Succeeded)
        {
            return (checkResult, null);
        }

        var user = await _repository.FindByNameAsync(userName);
        var token = await BuildTokenAsync(user!);
        return (SignInResult.Success, token);
    }

    private async Task<string> BuildTokenAsync(User user)
    {
        var roles = await _repository.GetRolesAsync(user);
        var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, user.Id.ToString()) };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return _tokenService.BuildToken(claims, _optJwt.Value);
    }
}