using Dotnet9.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Dotnet9.Blogs;

public class BlogPostAppService : Dotnet9AppService, IBlogPostAppService
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly BlogPostManager _blogPostManager;

    public BlogPostAppService(IBlogPostRepository blogPostRepository, BlogPostManager blogPostManager)
    {
        _blogPostRepository = blogPostRepository;
        _blogPostManager = blogPostManager;
    }

    public async Task<BlogPostDto> GetAsync(Guid id)
    {
        var blogPost = await _blogPostRepository.GetAsync(id);
        return ObjectMapper.Map<BlogPost, BlogPostDto>(blogPost);
    }

    public async Task<PagedResultDto<BlogPostDto>> GetListAsync(GetBlogPostListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(BlogPost.Title);
        }

        var blogPosts = await _blogPostRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting,
            input.Filter);

        var totalCount = input.Filter == null
            ? await _blogPostRepository.CountAsync()
            : await _blogPostRepository.CountAsync(
                blogPost => blogPost.Title.Contains(input.Filter)
                            || blogPost.Slug.Contains(input.Filter)
                            || blogPost.Content.Contains(input.Filter));

        return new PagedResultDto<BlogPostDto>(totalCount,
            ObjectMapper.Map<List<BlogPost>, List<BlogPostDto>>(blogPosts));
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
        var blogPost = await _blogPostRepository.GetAsync(id);

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

        await _blogPostRepository.UpdateAsync(blogPost);
    }

    [Authorize(Dotnet9Permissions.BlogPosts.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        await _blogPostRepository.DeleteAsync(id);
    }
}