namespace Dotnet9.Service.Domain.Aggregates.FriendlyLinks;

public class FriendlyLinkManager : IScopedDependency
{
    private readonly IFriendlyLinkRepository _repository;

    public FriendlyLinkManager(IFriendlyLinkRepository repository)
    {
        _repository = repository;
    }

    public async Task<FriendlyLink> CreateAsync(Guid? id, int sequenceNumber, string name, string url,
        string? description)
    {
        var isNew = id == null;
        FriendlyLink? oldData = null;
        if (isNew)
        {
            id = Guid.NewGuid();
        }
        else
        {
            oldData = await _repository.FindAsync(id!.Value);
            if (oldData == null)
            {
                throw new Exception($"不存在的链接: {id}");
            }
        }

        if (isNew)
        {
            oldData = new FriendlyLink(id.Value, sequenceNumber, name, url, description);
        }
        else
        {
            oldData!.ChangeIndex(sequenceNumber);
            oldData.ChangeDescription(description);
        }

        await ChangeNameAsync(isNew, oldData, name);
        await ChangeUrlAsync(isNew, oldData, url);

        return oldData;
    }

    public FriendlyLink CreateForSeed(int sequenceNumber, string name, string url, string? description,
        string kind)
    {
        return new FriendlyLink(Guid.NewGuid(), sequenceNumber, name, url, description);
    }

    public async Task ChangeNameAsync(bool isNew, FriendlyLink data, string newName)
    {
        Check.NotNull(data, nameof(data));
        Check.NotNullOrWhiteSpace(newName, nameof(newName), FriendlyLinkConsts.MaxNameLength,
            FriendlyLinkConsts.MinNameLength);

        var existData = await _repository.FindByNameAsync(newName);
        if ((isNew && existData != null) ||
            (isNew == false && existData != null && existData.Id != data!.Id))
        {
            throw new Exception($"存在同名的链接: {newName}");
        }

        data.ChangeName(newName);
    }

    public async Task ChangeUrlAsync(bool isNew, FriendlyLink data, string newUrl)
    {
        Check.NotNull(data, nameof(data));
        Check.NotNullOrWhiteSpace(newUrl, nameof(newUrl), FriendlyLinkConsts.MaxUrlLength,
            FriendlyLinkConsts.MinUrlLength);

        var existData = await _repository.FindByUrlAsync(newUrl);
        if ((isNew && existData != null) ||
            (isNew == false && existData != null && existData.Id != data!.Id))
        {
            throw new Exception($"存在相同URL的链接: {newUrl}");
        }

        data.ChangeUrl(newUrl);
    }
}