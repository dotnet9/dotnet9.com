namespace Dotnet9.Service.Application.Blogs.Commands;

public record CreateBlogSearchCountCommand(string Keywords, bool IsEmpty, string Ip, DateTime CreationTime) : Command
{
};