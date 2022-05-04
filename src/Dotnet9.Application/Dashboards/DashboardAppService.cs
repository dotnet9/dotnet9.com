using Dotnet9.Application.Contracts.Dashboards;
using Dotnet9.Domain.ActionLogs;
using Dotnet9.Domain.Blogs;

namespace Dotnet9.Application.Dashboards;

public class DashboardAppService : IDashboardAppService
{
    private readonly IActionLogRepository _actionLogRepository;
    private readonly IBlogPostRepository _blogPostRepository;

    public DashboardAppService(IBlogPostRepository blogPostRepository, IActionLogRepository actionLogRepository)
    {
        _blogPostRepository = blogPostRepository;
        _actionLogRepository = actionLogRepository;
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
}