namespace Dotnet9.Service.Actors;

public interface IFriendlyLinkActor : IActor
{
    Task<IEnumerable<FriendlyLink>> GetListAsync();
}