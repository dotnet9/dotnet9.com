namespace IdentityService.WebAPI.Controllers.UserAdmin;

public record UserDto(Guid Id, string? UserName, string? PhoneNumber, DateTime? CreationTime)
{
    public static UserDto Create(User user)
    {
        return new UserDto(user.Id, user.UserName, user.PhoneNumber, user.CreationTime);
    }
}