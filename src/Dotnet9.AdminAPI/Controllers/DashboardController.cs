using AutoMapper;
using Dotnet9.Application.Contracts;
using Dotnet9.Application.Contracts.ActionLogs;
using Dotnet9.Application.Contracts.Dashboards;
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
        return await _dashboardAppService.GetDashboardAsync();
    }

    [HttpGet("GetActionLog")]
    [AllowAnonymous]
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