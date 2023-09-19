namespace Dotnet9.WebAPI.ViewModel.Menus;

public class MenuItemDto
{
    public string? Name { get; set; }

    public string? Path { get; set; }

    public string? Component { get; set; }

    public string? Icon { get; set; }

    public bool Hidden { get; set; }

    public List<MenuItemDto>? Children { get; set; }
}