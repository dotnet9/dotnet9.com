using AutoMapper;
using Dotnet9.Application.Contracts.Blogs;
using Dotnet9.Core;
using Dotnet9.Domain.Albums;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Categories;
using Dotnet9.Domain.Repositories;

namespace Dotnet9.Application.Blogs;

public class BlogPostAppService : IBlogPostAppService
{
    private readonly IAlbumRepository _albumRepository;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IQueryCountRepository _queryCountRepository;
    private readonly IViewCountRepository _viewCountRepository;

    public BlogPostAppService(IBlogPostRepository blogPostRepository,
        IAlbumRepository albumRepository,
        ICategoryRepository categoryRepository,
        IViewCountRepository viewCountRepository,
        IQueryCountRepository queryCountRepository,
        IMapper mapper)
    {
        _blogPostRepository = blogPostRepository;
        _albumRepository = albumRepository;
        _categoryRepository = categoryRepository;
        _viewCountRepository = viewCountRepository;
        _queryCountRepository = queryCountRepository;
        _mapper = mapper;
    }

    public async Task<BlogPostViewModel?> FindBySlugAsync(string slug)
    {
        var blogPostWithDetails =
            await _blogPostRepository.GetBlogPostAsync(x => x.Slug == slug, x => x.Id, SortDirectionKind.Ascending);
        if (blogPostWithDetails == null) return null;

        var vm = new BlogPostViewModel
            {BlogPost = _mapper.Map<BlogPostWithDetails, BlogPostWithDetailsDto>(blogPostWithDetails)};

        var previewPost = await _blogPostRepository.GetBlogPostBriefAsync(
            x => x.CreateDate < blogPostWithDetails.CreateDate,
            x => x.CreateDate, SortDirectionKind.Descending);
        var nextPost = await _blogPostRepository.GetBlogPostBriefAsync(
            x => x.CreateDate > blogPostWithDetails.CreateDate,
            x => x.CreateDate, SortDirectionKind.Ascending);
        if (!blogPostWithDetails.AlbumNames.IsNullOrEmpty())
        {
            var album = await _albumRepository.GetAsync(x => x.Name == blogPostWithDetails.AlbumNames!.First().Name);
            if (album != null)
            {
                var sameAlbumPost = await _blogPostRepository.SelectBlogPostBriefAsync(4, 1,
                    x => x.Albums != null && x.Albums.Any(d => d.AlbumId == album.Id), x => x.CreateDate,
                    SortDirectionKind.Descending);
                vm.SameAlbumBlogPosts = _mapper.Map<List<BlogPostBrief>, List<BlogPostBriefDto>>(sameAlbumPost.Item1);
            }
        }

        if (!blogPostWithDetails.CategoryNames.IsNullOrEmpty())
        {
            var category =
                await _categoryRepository.GetAsync(x => x.Name == blogPostWithDetails.CategoryNames!.First().Name);
            if (category != null)
            {
                var sameCategoryPost = await _blogPostRepository.SelectBlogPostBriefAsync(4, 1,
                    x => x.Categories != null && x.Categories.Any(d => d.CategoryId == category.Id), x => x.CreateDate,
                    SortDirectionKind.Descending);
                vm.SameCategoryBlogPosts =
                    _mapper.Map<List<BlogPostBrief>, List<BlogPostBriefDto>>(sameCategoryPost.Item1);
            }
        }

        if (previewPost != null) vm.PreviewBlogPost = _mapper.Map<BlogPostBrief, BlogPostBriefDto>(previewPost);

        if (nextPost != null) vm.NextBlogPost = _mapper.Map<BlogPostBrief, BlogPostBriefDto>(nextPost);

        return vm;
    }

    public async Task<RecommendViewModel> GetRecommendBlogPostAsync()
    {
        var vm = new RecommendViewModel();

        var recommend =
            await _blogPostRepository.SelectBlogPostBriefAsync(x => x.InBanner, x => x.CreateDate,
                SortDirectionKind.Descending);

        if (recommend != null)
            vm.Items =
                _mapper.Map<List<BlogPostBrief>, List<BlogPostBriefDto>>(recommend);
        return vm;
    }

    public async Task<List<BlogPostForSitemap>> GetListBlogPostForSitemapAsync()
    {
        var blogPosts = await _blogPostRepository.SelectAsync();
        return _mapper.Map<List<BlogPost>, List<BlogPostForSitemap>>(blogPosts);
    }


    public async Task<bool> AddOrUpdateQueryCountAsync(QueryCountForCreationOrUpdateDto queryCountForCreationOrUpdate)
    {
        var existCount = await _queryCountRepository.GetAsync(x =>
            x.Original == queryCountForCreationOrUpdate.Original && x.IP == queryCountForCreationOrUpdate.IP &&
            x.Key == queryCountForCreationOrUpdate.Key);
        if (existCount != null)
        {
            existCount.Count++;
            existCount.UpdateDate = DateTime.Now;
            await _queryCountRepository.UpdateAsync(existCount);
        }
        else
        {
            queryCountForCreationOrUpdate.Count = 1;
            var countForDb = _mapper.Map<QueryCountForCreationOrUpdateDto, QueryCount>(queryCountForCreationOrUpdate);
            countForDb.CreateDate = DateTime.Now;
            await _queryCountRepository.InsertAsync(countForDb);
        }

        return true;
    }

    public async Task<bool> AddOrUpdateViewCountAsync(ViewCountForCreationOrUpdateDto viewCountForCreationOrUpdate)
    {
        var existCount = await _viewCountRepository.GetAsync(x =>
            x.Original == viewCountForCreationOrUpdate.Original && x.IP == viewCountForCreationOrUpdate.IP &&
            x.Url == viewCountForCreationOrUpdate.Url);
        if (existCount != null)
        {
            existCount.Count++;
            existCount.UpdateDate = DateTime.Now;
            await _viewCountRepository.UpdateAsync(existCount);
        }
        else
        {
            viewCountForCreationOrUpdate.Count = 1;
            var countForDb = _mapper.Map<ViewCountForCreationOrUpdateDto, ViewCount>(viewCountForCreationOrUpdate);
            countForDb.CreateDate = DateTime.Now;
            await _viewCountRepository.InsertAsync(countForDb);
        }

        return true;
    }
}