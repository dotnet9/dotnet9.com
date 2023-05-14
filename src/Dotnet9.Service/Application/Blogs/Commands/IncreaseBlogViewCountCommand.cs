namespace Dotnet9.Service.Application.Blogs.Commands;

public record IncreaseBlogViewCountCommand : DomainCommand
{
    public string Slug { get; set; }
}