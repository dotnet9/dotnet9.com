namespace Dotnet9.Web.Service.Links;

public interface ILinkService
{
    Task<List<LinkBrief>?> GetListAsync();
}