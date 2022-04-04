using Dotnet9.Application.Contracts.Timelines;

namespace Dotnet9.Web.ViewModels.Timelines;

public class TimelineViewModel
{
    public Dictionary<string, List<TimelineDto>>? Timelines { get; set; }
}