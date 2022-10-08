namespace Dotnet9.Web.Service.Links;

internal class LinkService : ILinkService
{
    private readonly Dotnet9DbContext _dbContext;

    public LinkService(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<LinkBrief>?> GetListAsync()
    {
        return await _dbContext.Links!.AsNoTracking().Where(x => x.Kind == LinkKind.Friend)
            .OrderBy(x => x.SequenceNumber)
            .Select(x => new LinkBrief(x.Name, x.Url, x.Description)).ToListAsync();
    }
}