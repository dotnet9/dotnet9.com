namespace Dotnet9.Service.Application.Blogs.Queries;

public record TopSearchKeywordsQuery : Query<List<BlogSearchCountDto>>
{
    public override List<BlogSearchCountDto> Result { get; set; } = default!;
}