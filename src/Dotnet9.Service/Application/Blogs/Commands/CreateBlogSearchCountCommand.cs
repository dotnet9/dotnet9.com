namespace Dotnet9.Service.Application.Blogs.Commands;

public record CreateBlogSearchCountCommand(string Keywords, string Ip, DateTime CreationTime) : Command
{
};