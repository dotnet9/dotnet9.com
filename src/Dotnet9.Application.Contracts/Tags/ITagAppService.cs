namespace Dotnet9.Application.Contracts.Tags;

public interface ITagAppService
{
    Task<TagViewModel?> GetTagAsync(string? tagName);
}