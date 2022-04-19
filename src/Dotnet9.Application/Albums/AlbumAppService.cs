using AutoMapper;
using Dotnet9.Application.Contracts.Albums;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Core;
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

    public async Task<AlbumViewModel?> GetAlbumAsync(string? slug)
    {
        if (slug.IsNullOrWhiteSpace())
        {
            var albumCounts = await _albumRepository.GetListCountAsync();

            return new AlbumViewModel {Albums = _mapper.Map<List<AlbumCount>, List<AlbumCountDto>>(albumCounts) };
        }

        var album = await _albumRepository.GetAsync(x => x.Slug == slug);
        if (album == null) return null;
        var vm = new AlbumViewModel {Name = album.Name};

        var blogPostList =
            await _blogPostRepository.SelectBlogPostBriefAsync(
                x => x.Albums != null && x.Albums.Any(d => d.AlbumId == album.Id), x => x.CreateDate,
                SortDirectionKind.Ascending);
        if (!blogPostList.IsNullOrEmpty())
            vm.BlogPosts = _mapper.Map<List<BlogPostBrief>, List<BlogPostBriefDto>>(blogPostList!);

        return vm;
    }

    public async Task<List<AlbumCountDto>> GetListCountAsync()
    {
        var categories = await _albumRepository.GetListCountAsync();

        return _mapper.Map<List<AlbumCount>, List<AlbumCountDto>>(categories);
    }
}