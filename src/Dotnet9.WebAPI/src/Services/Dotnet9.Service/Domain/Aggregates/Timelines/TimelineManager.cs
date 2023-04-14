namespace Dotnet9.Service.Domain.Aggregates.Timelines;

public class TimelineManager : IScopedDependency
{
    private readonly ITimelineRepository _repository;

    public TimelineManager(ITimelineRepository repository)
    {
        _repository = repository;
    }

    public async Task<Timeline> CreateAsync(Guid? id, DateTime time, string title, string content)
    {
        var isNew = id == null;
        Timeline? oldData = null;
        if (isNew)
        {
            id = Guid.NewGuid();
        }
        else
        {
            oldData = await _repository.FindAsync(id!.Value);
            if (oldData == null)
            {
                throw new Exception($"不存在的时间线: {id}");
            }
        }

        if (isNew)
        {
            oldData = new Timeline(id.Value, time, title, content);
        }
        else
        {
            oldData!.ChangeTime(time);
            oldData.ChangeTitle(title);
            oldData.ChangeContent(content);
        }

        return oldData;
    }

    public Timeline CreateForSeed(DateTime time, string title, string content)
    {
        return new Timeline(Guid.NewGuid(), time, title, content);
    }
}