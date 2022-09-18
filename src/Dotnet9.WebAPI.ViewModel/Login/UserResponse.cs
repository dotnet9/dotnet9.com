namespace Dotnet9.WebAPI.ViewModel.Login;

public class UserResponse
{
    public string? Name { get; set; }
    public string? Avatar { get; set; }
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public string? Signature { get; set; }
    public string? Title { get; set; }
    public string? Group { get; set; }
    public Dictionary<string, string>? Tags { get; set; }
    public int NotifyCount { get; set; }
    public int UnreadCount { get; set; }
    public string? Country { get; set; }
    public string? Access { get; set; }
    public Geographic? Geographic { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
}

public record Geographic(KeyValue Province, KeyValue City);

public record KeyValue(string Lable, string Key);