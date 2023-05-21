namespace Dotnet9.Service.Application.Blogs.Commands;

public record CreateBlogViewCountCommand(string Slug, string Ip, DateTime CreationTime) : Command
{
};