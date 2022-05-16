using Dotnet9.Application.Contracts.Abouts;
using Dotnet9.Application.Contracts.Caches;
using Dotnet9.Application.Contracts.Timelines;
using Dotnet9.Web.ViewModels.Timelines;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable once CheckNamespace
namespace Dotnet9.Web.Controllers;

public class TimelineController : Controller
{
    private readonly ICacheService _cacheService;
    private readonly ITimelineAppService _timelineAppService;

    public TimelineController(ITimelineAppService timelineAppService, ICacheService cacheService)
    {
        _timelineAppService = timelineAppService;
        _cacheService = cacheService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var cacheKey = $"{nameof(TimelineController)}-{nameof(Index)}";
        var cacheData = await _cacheService.GetAsync<TimelineViewModel>(cacheKey);
        if (cacheData != null) return View(cacheData);

        var timelines = await _timelineAppService.ListAllAsync();
        cacheData = new TimelineViewModel();
        cacheData.Timelines = new Dictionary<string, List<TimelineDto>>();
        foreach (var timelineDto in timelines)
        {
            var key = timelineDto.Time.ToString("yyyy-MM");
            if (!cacheData.Timelines.ContainsKey(key)) cacheData.Timelines[key] = new List<TimelineDto>();

            cacheData.Timelines[key].Add(timelineDto);
        }

        await _cacheService.ReplaceAsync(cacheKey, cacheData!, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(30));

        return View(cacheData);
    }
}