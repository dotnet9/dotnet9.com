using Dotnet9.Albums;
using Dotnet9.Categories;
using Dotnet9.Permissions;
using Dotnet9.Tags;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Dotnet9.Blogs;

public class BlogPostAppService : Dotnet9AppService, IBlogPostAppService
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly BlogPostManager _blogPostManager;
    private readonly IAlbumRepository _albumRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ITagRepository _tagRepository;

    public BlogPostAppService(
        IBlogPostRepository blogPostRepository,
        BlogPostManager blogPostManager,
        IAlbumRepository albumRepository,
        ICategoryRepository categoryRepository,
        ITagRepository tagRepository)
    {
        _blogPostRepository = blogPostRepository;
        _blogPostManager = blogPostManager;
        _albumRepository = albumRepository;
        _categoryRepository = categoryRepository;
        _tagRepository = tagRepository;
    }

    public async Task<BlogPostDto> GetAsync(Guid id)
    {
        var blogPost = await _blogPostRepository.GetAsync(id);
        return ObjectMapper.Map<BlogPost, BlogPostDto>(blogPost);
    }

    public async Task<BlogPostDto> GetAsync([NotNull] string blogPostSlug)
    {
        var blogPost = await _blogPostRepository.FindBySlugAsync(blogPostSlug);
        return ObjectMapper.Map<BlogPostWithDetails, BlogPostDto>(blogPost);
    }

    public async Task<PagedResultDto<BlogPostDto>> GetListAsync(GetBlogPostListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(BlogPost.Title);
        }

        var blogPosts = await _blogPostRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting,
            input.Filter, input.Album, input.Category, input.Tag);

        var totalCount = await _blogPostRepository.GetCountAsync(input.Filter, input.Album, input.Category, input.Tag);

        return new PagedResultDto<BlogPostDto>(totalCount,
            ObjectMapper.Map<List<BlogPostWithDetails>, List<BlogPostDto>>(blogPosts));
    }

    [Authorize(Dotnet9Permissions.BlogPosts.Create)]
    public async Task<BlogPostDto> CreateAsync(CreateBlogPostDto input)
    {
        var album = await _blogPostManager.CreateAsync(
            input.Title,
            input.Slug,
            input.ShortDescription,
            input.Content,
            input.CoverImageUrl,
            input.CopyrightType,
            input.Original,
            input.OriginalTitle,
            input.OriginalLink);

        await _blogPostRepository.InsertAsync(album);

        return ObjectMapper.Map<BlogPost, BlogPostDto>(album);
    }

    [Authorize(Dotnet9Permissions.BlogPosts.Edit)]
    public async Task UpdateAsync(Guid id, UpdateBlogPostDto input)
    {
        var blogPost = await _blogPostRepository.GetAsync(id, includeDetails: true);

        if (blogPost.Title != input.Title)
        {
            await _blogPostManager.ChangeTitleAsync(blogPost, input.Title);
        }

        if (blogPost.Slug != input.Slug)
        {
            await _blogPostManager.ChangeSlugAsync(blogPost, input.Slug);
        }

        blogPost.ShortDescription = input.ShortDescription;
        blogPost.Content = input.Content;
        blogPost.CoverImageUrl = input.CoverImageUrl;
        blogPost.CopyrightType = input.CopyrightType;
        blogPost.Original = input.Original;
        blogPost.OriginalTitle = input.OriginalTitle;
        blogPost.OriginalLink = input.OriginalLink;

        await _blogPostManager.SetAlbumsAsync(blogPost, input.AlbumNames);
        await _blogPostManager.SetCategoriesAsync(blogPost, input.CategoryNames);
        await _blogPostManager.SetTagsAsync(blogPost, input.TagNames);

        await _blogPostRepository.UpdateAsync(blogPost);
    }

    [Authorize(Dotnet9Permissions.BlogPosts.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _blogPostRepository.DeleteAsync(id);
    }

    public async Task<ListResultDto<AlbumLookupDto>> GetAlbumLookupAsync()
    {
        var albums = await _albumRepository.GetListAsync();

        return new ListResultDto<AlbumLookupDto>(
            ObjectMapper.Map<List<Album>, List<AlbumLookupDto>>(albums)
        );
    }

    public async Task<ListResultDto<CategoryLookupDto>> GetCategoryLookupAsync()
    {
        var categories = await _categoryRepository.GetListAsync();

        return new ListResultDto<CategoryLookupDto>(
            ObjectMapper.Map<List<Category>, List<CategoryLookupDto>>(categories)
        );
    }

    public async Task<ListResultDto<TagLookupDto>> GetTagLookupAsync()
    {
        var tags = await _tagRepository.GetListAsync();

        return new ListResultDto<TagLookupDto>(
            ObjectMapper.Map<List<Tag>, List<TagLookupDto>>(tags)
        );
    }
}