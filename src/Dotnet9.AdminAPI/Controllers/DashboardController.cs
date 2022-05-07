using AutoMapper;
using Dotnet9.Application.Contracts;
using Dotnet9.Application.Contracts.ActionLogs;
using Dotnet9.Application.Contracts.Dashboards;
using Dotnet9.Extensions.CountSystemInfo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.AdminAPI.Controllers;

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
    public async Task<DashboardViewModel> Count()
    {
        var vm = await _dashboardAppService.GetDashboardAsync();
        var systemInfo = PerfCounter.List.LastOrDefault();
        vm.CpuLoad = (int?)systemInfo?.CpuLoad;
        vm.MemoryUsage = (int?)systemInfo?.MemoryUsage;
        vm.DiskRead = $"{systemInfo?.DiskRead:N1}KB/s";
        vm.DiskWrite = $"{systemInfo?.DiskWrite:N1}KB/s";
        return vm;
    }

    [HttpGet("GetActionLog")]
    public async Task<PageDto<ActionLogDto>> GetActionLogs(int page)
    {
        var result = await _dashboardAppService.GetActionLogAsync(page);

        return new PageDto<ActionLogDto>
        {
            Total = result.Total,
            Datas = result.ActionLogDtos
        };
    }
}