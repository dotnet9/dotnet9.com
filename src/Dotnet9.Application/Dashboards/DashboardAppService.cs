using AutoMapper;
using Dotnet9.Application.Contracts.ActionLogs;
using Dotnet9.Application.Contracts.Dashboards;
using Dotnet9.Domain.ActionLogs;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Repositories;

namespace Dotnet9.Application.Dashboards;

public class DashboardAppService : IDashboardAppService
{
    private readonly IActionLogRepository _actionLogRepository;
    private readonly IMapper _mapper;
    private readonly IBlogPostRepository _blogPostRepository;

    public DashboardAppService(IBlogPostRepository blogPostRepository, IActionLogRepository actionLogRepository,
        IMapper mapper)
    {
        _blogPostRepository = blogPostRepository;
        _actionLogRepository = actionLogRepository;
        _mapper = mapper;
    }

    public async Task<DashboardViewModel> GetDashboardAsync()
    {
        return new DashboardViewModel
        {
            PostCount = await _blogPostRepository.CountAsync(),
            IPOf24Hours = await _actionLogRepository.CountIPIn24HoursAsync(),
            NotFoundRequestIn24Hours = await _actionLogRepository.CountNotFoundIn24HoursAsync()
        };
    }


    public async Task<ActionLogViewModel> GetActionLogAsync(int page)
    {
        var logs = await _actionLogRepository.SelectAsync(15, page, x => x.Id > 0, x => x.CreateDate,
            SortDirectionKind.Descending);
        var vm = new ActionLogViewModel()
        {
            Total = logs.Item2,
            ActionLogDtos = _mapper.Map<List<ActionLog>, List<ActionLogDto>>(logs.Item1)
        };
        return vm;
    }
}