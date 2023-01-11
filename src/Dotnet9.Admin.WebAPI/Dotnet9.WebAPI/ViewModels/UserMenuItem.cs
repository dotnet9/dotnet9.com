namespace Dotnet9.WebAPI.ViewModels;

public class UserMenuItem
{
    public string? Name { get; set; }
    public string? Path { get; set; }
    public string? Component { get; set; }
    public string? Icon { get; set; }
    public bool? Hidden { get; set; }
    public List<UserMenuItem>? Children { get; set; }
}