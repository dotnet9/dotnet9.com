using System.Security.Claims;

namespace Dotnet9.JWT;

public interface ITokenService
{
    string BuildToken(IEnumerable<Claim> claims, JWTOptions options);
}