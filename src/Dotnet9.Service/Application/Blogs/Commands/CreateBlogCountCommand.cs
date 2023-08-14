namespace Dotnet9.Service.Application.Blogs.Commands;

public record CreateBlogCountCommand(Guid BlogId, string Ip, BlogCountKind Kind) : Command
{
};