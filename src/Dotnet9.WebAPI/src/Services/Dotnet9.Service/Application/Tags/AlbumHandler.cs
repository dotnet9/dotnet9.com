namespace Dotnet9.Service.Application.Tags;

public class TagHandler
{
    private readonly ITagRepository _repository;

    public TagHandler(ITagRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetListAsync(TagQuery query, CancellationToken cancellationToken)
    {
        var categories = await _repository.GetHotTagBriefListAsync();

        query.Result = new PaginatedListBase<TagBrief>()
        {
            Result = categories
        };
    }
}
