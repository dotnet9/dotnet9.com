using Dotnet9.Application.Contracts.Timelines;

namespace Dotnet9.Application.Contracts.Abouts;

public interface ITimelineAppService
{
    Task<List<TimelineDto>> ListAllAsync();
}