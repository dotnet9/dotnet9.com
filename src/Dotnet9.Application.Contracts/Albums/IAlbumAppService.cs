using Dotnet9.Application.Contracts.Blogs;

namespace Dotnet9.Application.Contracts.Albums;

public interface IAlbumAppService
{
    Task<AlbumDto?> GetAlbumAsync(string slug);
    Task<List<AlbumCountDto>> GetListCountAsync();

    Task<List<BlogPostWithDetailsDto>?> GetBlogPostListAsync(string albumSlug);
}