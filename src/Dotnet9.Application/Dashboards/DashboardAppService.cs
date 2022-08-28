using AutoMapper;
using Dotnet9.Application.Contracts.ActionLogs;
using Dotnet9.Application.Contracts.Dashboards;
using Dotnet9.Domain.ActionLogs;
using Dotnet9.Domain.Blogs;

namespace Dotnet9.Application.Dashboards;

public class DashboardAppService : IDashboardAppService
{
    private readonly IActionLogRepository _actionLogRepository;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IMapper _mapper;

    public DashboardAppService(IBlogPostRepository blogPostRepository, IActionLogRepository actionLogRepository,
        IMapper mapper)
    {
        _blogPostRepository = blogPostRepository;
        _actionLogRepository = actionLogRepository;
        _mapper = mapper;
    }

    public async Task<DashboardViewModel> GetDashboardAsync(string? latestTime)
    {
        var vm = new DashboardViewModel
        {
            SystemCountInfo = new SystemCountDto
            {
                PostCount = await _blogPostRepository.CountAsync(x => x.Id > 0),
                IPOf24Hours = await _actionLogRepository.CountIPIn24HoursAsync(),
                NotFoundRequestIn24Hours = await _actionLogRepository.CountNotFoundIn24HoursAsync()
            }
        };

        var top10Searchs = await _actionLogRepository.CountTop10SearchAsync();
        vm.Top10Searches = _mapper.Map<Top10Search, Top10SearchDto>(top10Searchs);
        var top10AccessPages = await _actionLogRepository.CountTop10AccessPagesAsync();
        vm.Top10AccessPages = _mapper.Map<Top10AccessPage, Top10AccessPageDto>(top10AccessPages);
        var latestTimeForRepository = default(DateTimeOffset);
        if (latestTime != null)
        {
            DateTimeOffset.TryParse(latestTime, out latestTimeForRepository);
        }

        var latestActionLog = await _actionLogRepository.GetLatestActionLogAsync(latestTimeForRepository);
        vm.LatestLogs = _mapper.Map<LatestActionLog, LatestActionLogDto>(latestActionLog);
        return vm;
    }
}