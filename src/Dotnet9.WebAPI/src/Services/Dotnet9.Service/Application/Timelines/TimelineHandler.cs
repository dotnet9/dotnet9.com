namespace Dotnet9.Service.Application.Timelines;

public class TimelineHandler
{
    private readonly ITimelineRepository _repository;

    public TimelineHandler(ITimelineRepository repository)
    {
        _repository = repository;
    }

    [EventHandler]
    public async Task GetListAsync(TimelineQuery query, CancellationToken cancellationToken)
    {
        var datas = await _repository.GetListAsync();

        query.Result = new PaginatedListBase<TimelineDto>()
        {
            Result = datas
        };
    }
}