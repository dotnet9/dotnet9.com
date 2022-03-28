namespace Dotnet9.Application.Contracts.Tags;

public interface ITagAppService
{
    Task<List<TagCountDto>> GetListCountAsync();
}