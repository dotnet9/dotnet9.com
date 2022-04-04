using AutoMapper;
using Dotnet9.Application.Contracts.Abouts;
using Dotnet9.Application.Contracts.Timelines;
using Dotnet9.Domain.Timelines;

namespace Dotnet9.Application.Timelines;

public class TimelineAppService : ITimelineAppService
{
    private readonly IMapper _mapper;
    private readonly ITimelineRepository _timelineRepository;

    public TimelineAppService(ITimelineRepository timelineRepository, IMapper mapper)
    {
        _timelineRepository = timelineRepository;
        _mapper = mapper;
    }

    public async Task<List<TimelineDto>> ListAllAsync()
    {
        var urlLinks = await _timelineRepository.GetListAsync();

        return _mapper.Map<List<Timeline>, List<TimelineDto>>(urlLinks);
    }
}