using Dotnet9.Application.Contracts.Dashboards;

namespace Dotnet9.Web.Controllers;

[Route("api/dashboard")]
[Authorize]
[ApiController]
public class DashboardController : ControllerBase
{
    private readonly IDashboardAppService _dashboardAppService;

    public DashboardController(IDashboardAppService dashboardAppService, IMapper mapper)
    {
        _dashboardAppService = dashboardAppService;
    }

    [HttpGet("count")]
    public async Task<DashboardViewModel> Count(string? request)
    {
        var vm = await _dashboardAppService.GetDashboardAsync(request);

        var systemInfo = PerfCounter.List.LastOrDefault();
        vm.SystemCountInfo!.CpuLoad = (int?)systemInfo?.CpuLoad;
        vm.SystemCountInfo!.MemoryUsage = (int?)systemInfo?.MemoryUsage;
        vm.SystemCountInfo!.DiskRead = $"{systemInfo?.DiskRead:N1}KB/s";
        vm.SystemCountInfo!.DiskWrite = $"{systemInfo?.DiskWrite:N1}KB/s";
        return vm;
    }
}