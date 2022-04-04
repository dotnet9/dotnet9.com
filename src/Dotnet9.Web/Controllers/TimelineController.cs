using Dotnet9.Application.Contracts.Abouts;
using Dotnet9.Application.Contracts.Timelines;
using Dotnet9.Web.ViewModels.Timelines;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet9.Web.Controllers;

public class TimelineController : Controller
{
    private readonly ITimelineAppService _timelineAppService;

    public TimelineController(ITimelineAppService timelineAppService)
    {
        _timelineAppService = timelineAppService;
    }

    public async Task<IActionResult> Index()

    {
        var timelines = await _timelineAppService.ListAllAsync();
        var vm = new TimelineViewModel();
        vm.Timelines = new Dictionary<string, List<TimelineDto>>();
        foreach (var timelineDto in timelines)
        {
            var key = timelineDto.Time.ToString("yyyy-MM");
            if (!vm.Timelines.ContainsKey(key)) vm.Timelines[key] = new List<TimelineDto>();

            vm.Timelines[key].Add(timelineDto);
        }

        return View(vm);
    }
}