namespace Dotnet9.Service.Application.Timelines.Queries;

public record TimelineQuery : ItemsQueryBase<PaginatedListBase<TimelineDto>>
{
    public override PaginatedListBase<TimelineDto> Result { get; set; } = default!;
}