using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet9.Domain.Users;

public class User : EntityBase
{
    public string? Name { get; set; }

    public string? Account { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    // Used for EF Core
    public int Role { get; set; }

    // Used for code
    [NotMapped]
    public UserRoleKind? RoleKind
    {
        get => (UserRoleKind?) Enum.Parse(typeof(UserRoleKind), Role.ToString());
        set
        {
            if (value.HasValue)
                Role = (int) value.Value;
            else
                Role = (int) UserRoleKind.User;
        }
    }
}

public enum UserRoleKind
{
    Admin,
    Author,
    User
}

public class UserRoleConst
{
    public const string Admin = "Admin";
    public const string Author = "Author";
    public const string User = "User";
}