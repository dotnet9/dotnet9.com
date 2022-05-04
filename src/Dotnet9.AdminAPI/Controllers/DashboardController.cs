using AutoMapper;
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
    [AllowAnonymous]
    public async Task<DashboardViewModel> Count()
    {
        return await _dashboardAppService.GetDashboardAsync();
    }
}