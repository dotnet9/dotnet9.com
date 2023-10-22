namespace Dotnet9.Application.User.Dtos;

public class AuthAccountPageQueryInput : Pagination
{
    /// <summary>
    /// 昵称
    /// </summary>
    public string Name { get; set; }
}