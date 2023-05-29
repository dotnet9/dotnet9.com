namespace Dotnet9.Contracts.Auth;

public class JwtOptions
{
    public string Secret { get; set; }

    public int EffectiveHours { get; set; }
}