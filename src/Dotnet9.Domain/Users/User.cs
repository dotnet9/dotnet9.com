namespace Dotnet9.Domain.Users;

public class User : IdentityUser<long>
{
    public DateTime CreationTime { get; set; }

    public string? NickName { get; set; }
}