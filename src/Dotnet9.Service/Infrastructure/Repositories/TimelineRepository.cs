namespace Dotnet9.Service.Infrastructure.Repositories;

public class TimelineRepository : Repository<Dotnet9DbContext, Timeline, Guid>, ITimelineRepository
{
    public TimelineRepository(Dotnet9DbContext context, IUnitOfWork unitOfWork) : base(context, unitOfWork)
    {
    }

    public async Task<List<TimelineDto>?> GetListAsync()
    {
        return (await Context.Set<Timeline>().ToListAsync()).Adapt<List<TimelineDto>>();
    }
}