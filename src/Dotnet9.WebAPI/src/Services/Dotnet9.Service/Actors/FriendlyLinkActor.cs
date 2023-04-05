namespace Dotnet9.Service.Actors;

public class FriendlyLinkActor : Actor, IFriendlyLinkActor
{
    readonly IFriendlyLinkRepository _friendlyLinkRepository;

    public FriendlyLinkActor(ActorHost host, IFriendlyLinkRepository friendlyLinkRepository) : base(host)
    {
        _friendlyLinkRepository = friendlyLinkRepository;
    }

    public async Task<IEnumerable<FriendlyLink>> GetListAsync()
    {
        var data = await _friendlyLinkRepository.GetListAsync();
        return data;
    }
}