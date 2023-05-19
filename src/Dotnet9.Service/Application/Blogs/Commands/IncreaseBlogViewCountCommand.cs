namespace Dotnet9.Service.Application.Blogs.Commands;

public record IncreaseBlogViewCountCommand(string Slug) : Command{};