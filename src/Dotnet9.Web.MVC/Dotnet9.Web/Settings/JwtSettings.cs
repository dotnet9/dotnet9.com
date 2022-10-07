namespace Dotnet9.Web.Settings;

public class JwtSettings
{
    public string? ValidAudience { get; set; }

    public string? ValidIssuer { get; set; }

    public string? Secret { get; set; }
}