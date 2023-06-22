namespace Dotnet9.Service.Application.Blogs.Queries;

public record CountBriefQuery : Query<BlogCountBrief>
{
    public override BlogCountBrief Result { get; set; } = default!;
}