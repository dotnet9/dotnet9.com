using static Google.Rpc.Help.Types;

namespace Dotnet9.Service.Domain.Repositories;

public interface IFriendlyLinkRepository : IRepository<FriendlyLink, Guid>
{
    Task<FriendlyLink?> FindByIdAsync(Guid id);
    Task<FriendlyLink?> FindByNameAsync(string name);
    Task<FriendlyLink?> FindByUrlAsync(string url);
    Task<GetFriendlyLinkListResponse> GetFriendlyLinkListAsync(FriendlyLinksQuery query);
}