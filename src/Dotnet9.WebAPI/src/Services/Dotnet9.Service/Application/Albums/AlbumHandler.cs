namespace Dotnet9.Service.Application.Albums;

public class AlbumHandler
{
    private readonly IAlbumRepository _repository;

    public AlbumHandler(IAlbumRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetListAsync(AlbumsQuery query, CancellationToken cancellationToken)
    {
        var categories = _repository.GetAllBriefAsync();

        query.Result = new PaginatedListBase<AlbumBrief>()
        {
            Result = categories.Result
        };
    }
}