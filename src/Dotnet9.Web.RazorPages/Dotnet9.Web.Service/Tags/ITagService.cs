namespace Dotnet9.Web.Service.Tags;

public interface ITagService
{
    Task<List<TagBrief>> GetTagsAsync();
}