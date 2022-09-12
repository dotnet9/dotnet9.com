namespace Dotnet9.WebAPI.ViewModels.Timelines;

public record TimelineDto(Guid Id, DateTime Time, string Title, string Content);