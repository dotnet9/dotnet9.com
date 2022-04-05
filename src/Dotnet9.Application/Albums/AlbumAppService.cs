using AutoMapper;
using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Domain.Albums;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Repositories;

namespace Dotnet9.Application.Albums;

public class AlbumAppService : IAlbumAppService
{
    private readonly AlbumManager _albumManager;
    private readonly IAlbumRepository _albumRepository;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly IMapper _mapper;

    public AlbumAppService(IAlbumRepository albumRepository, AlbumManager albumManager,
        IBlogPostRepository blogPostRepository, IMapper mapper)
    {
        _albumRepository = albumRepository;
        _albumManager = albumManager;
        _blogPostRepository = blogPostRepository;
        _mapper = mapper;
    }

    public async Task<AlbumDto?> GetAlbumAsync(string slug)
    {
        var album = await _albumRepository.GetAsync(x => x.Slug == slug);
        return album == null ? null : _mapper.Map<Album, AlbumDto>(album);
    }

    public async Task<List<AlbumCountDto>> GetListCountAsync()
    {
        var categories = await _albumRepository.GetListCountAsync();

        return _mapper.Map<List<AlbumCount>, List<AlbumCountDto>>(categories);
    }

    public async Task<List<BlogPostWithDetailsDto>?> GetBlogPostListAsync(string albumSlug)
    {
        var album = await _albumRepository.FindBySlugAsync(albumSlug);
        if (album == null) return null;
        var blogPosts =
            await _blogPostRepository.SelectBlogPostAsync(
                x => x.Albums != null && x.Albums.Any(d => d.AlbumId == album.Id), x => x.CreateDate,
                SortDirectionKind.Ascending);
        return blogPosts == null
            ? null
            : _mapper.Map<List<BlogPostWithDetails>, List<BlogPostWithDetailsDto>>(blogPosts);
    }
}