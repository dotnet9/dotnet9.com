namespace Dotnet9.JWT;

public class TokenService : ITokenService
{
    public string BuildToken(IEnumerable<Claim> claims, JWTOptions options)
    {
        var expiryDuration = TimeSpan.FromSeconds(options.ExpireSeconds);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key!));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(options.Issuer, options.Audience, claims,
            expires: DateTime.Now.Add(expiryDuration), signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}