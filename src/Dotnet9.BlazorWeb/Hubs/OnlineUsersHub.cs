namespace Dotnet9.BlazorWeb.Hubs;

public class OnlineUsersHub : Hub
{
    private static readonly HashSet<string> Connections = new();

    public override async Task OnConnectedAsync()
    {
        Connections.Add(Context.ConnectionId);
        await base.OnConnectedAsync();
        await Clients.All.SendAsync("UserCountUpdated", Connections.Count);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Connections.Remove(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
        await Clients.All.SendAsync("UserCountUpdated", Connections.Count);
    }
}