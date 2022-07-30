using System.Security.Claims;

namespace IdentityService.Domain;

public class IdDomainService
{
    private readonly IOptions<JWTOptions> _optJwt;
    private readonly IIdRepository _repository;
    private readonly ITokenService _tokenService;

    public IdDomainService(IIdRepository repository, ITokenService tokenService, IOptions<JWTOptions> optJwt)
    {
        _repository = repository;
        _tokenService = tokenService;
        _optJwt = optJwt;
    }

    private async Task<SignInResult> CheckUserNameAndPwdAsync(string userName, string password)
    {
        User? user = await _repository.FindByNameAsync(userName);
        if (user == null)
        {
            return SignInResult.Failed;
        }

        SignInResult result = await _repository.CheckForSignInAsync(user, password, true);
        return result;
    }

    private async Task<SignInResult> CheckPhoneNumberAndPwdAsync(string phoneNumber, string password)
    {
        User? user = await _repository.FindByPhoneNumberAsync(phoneNumber);
        if (user == null)
        {
            return SignInResult.Failed;
        }

        SignInResult result = await _repository.CheckForSignInAsync(user, password, true);
        return result;
    }

    public async Task<(SignInResult Result, string? Token)> LoginByPhoneAndPwdAsync(string phoneNumber, string password)
    {
        SignInResult checkResult = await CheckPhoneNumberAndPwdAsync(phoneNumber, password);
        if (!checkResult.Succeeded)
        {
            return (checkResult, null);
        }

        User? user = await _repository.FindByPhoneNumberAsync(phoneNumber);
        string token = await BuildTokenAsync(user!);
        return (SignInResult.Success, token);
    }

    public async Task<(SignInResult Result, string? Token)> LoginByUserNameAndPwdAsync(string userName, string password)
    {
        SignInResult checkResult = await CheckUserNameAndPwdAsync(userName, password);
        if (!checkResult.Succeeded)
        {
            return (checkResult, null);
        }

        User? user = await _repository.FindByNameAsync(userName);
        string token = await BuildTokenAsync(user!);
        return (SignInResult.Success, token);
    }

    private async Task<string> BuildTokenAsync(User user)
    {
        IList<string> roles = await _repository.GetRolesAsync(user);
        List<Claim> claims = new() { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        return _tokenService.BuildToken(claims, _optJwt.Value);
    }
}