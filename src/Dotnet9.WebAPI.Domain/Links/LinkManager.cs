namespace Dotnet9.WebAPI.Domain.Links;

public class LinkManager
{
    private readonly ILinkRepository _repository;

    public LinkManager(ILinkRepository repository)
    {
        _repository = repository;
    }

    public async Task<Link> CreateAsync(Guid? id, int sequenceNumber, string name, string url, string? description,
        LinkKind kind)
    {
        var isNew = id == null;
        Link? oldData = null;
        if (isNew)
        {
            id = Guid.NewGuid();
        }
        else
        {
            oldData = await _repository.FindByIdAsync(id!.Value);
            if (oldData == null)
            {
                throw new Exception($"不存在的链接: {id}");
            }
        }

        if (isNew)
        {
            oldData = new Link(id.Value, sequenceNumber, name, url, description, kind);
        }
        else
        {
            oldData!.ChangeSequenceNumber(sequenceNumber);
            oldData.ChangeDescription(description);
            oldData.ChangeKind(kind);
        }

        await ChangeNameAsync(isNew, oldData, name);
        await ChangeUrlAsync(isNew, oldData, url);

        return oldData;
    }

    public async Task ChangeNameAsync(bool isNew, Link data, string newName)
    {
        Check.NotNull(data, nameof(data));
        Check.NotNullOrWhiteSpace(newName, nameof(newName), LinkConsts.MaxNameLength, LinkConsts.MinNameLength);

        var existData = await _repository.FindByNameAsync(newName);
        if ((isNew && existData != null) ||
            (isNew == false && existData != null && existData.Id != data!.Id))
        {
            throw new Exception($"存在同名的链接: {newName}");
        }

        data.ChangeName(newName);
    }

    public async Task ChangeUrlAsync(bool isNew, Link data, string newUrl)
    {
        Check.NotNull(data, nameof(data));
        Check.NotNullOrWhiteSpace(newUrl, nameof(newUrl), LinkConsts.MaxUrlLength, LinkConsts.MinUrlLength);

        var existData = await _repository.FindByUrlAsync(newUrl);
        if ((isNew && existData != null) ||
            (isNew == false && existData != null && existData.Id != data!.Id))
        {
            throw new Exception($"存在相同URL的链接: {newUrl}");
        }

        data.ChangeUrl(newUrl);
    }
}