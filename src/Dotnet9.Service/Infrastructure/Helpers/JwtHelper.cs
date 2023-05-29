namespace Dotnet9.Service.Infrastructure.Helpers;

public class JwtHelper
{
    public static string GeneratorAccessToken(ClaimsIdentity claimsIdentity, JwtOptions options)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(options.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Expires = DateTime.UtcNow.AddHours(options.EffectiveHours),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static ClaimsIdentity GetClaimsIdentity(User user)
    {
        return new ClaimsIdentity(new Claim[]
        {
            new(ClaimTypes.Name, user.Account),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        });
    }
}