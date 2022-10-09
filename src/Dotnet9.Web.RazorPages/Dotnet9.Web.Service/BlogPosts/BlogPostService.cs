﻿using Dotnet9.WebAPI.Domain.Categories;
using Microsoft.EntityFrameworkCore.Query;

namespace Dotnet9.Web.Service.BlogPosts;

internal class BlogPostService : IBlogPostService
{
    private readonly Dotnet9DbContext _dbContext;

    public BlogPostService(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetBlogPostBriefListResponse> GetBlogPostBriefListAsync(GetBlogPostBriefListRequest request)
    {
        IQueryable<BlogPost> query = _dbContext.BlogPosts!.AsQueryable();
        if (!request.Keywords.IsNullOrWhiteSpace())
        {
            query = query.Where(log =>
                EF.Functions.Like(log.Title, $"%{request.Keywords}%")
                || EF.Functions.Like(log.Slug, $"%{request.Keywords}%")
                || (log.Original != null && EF.Functions.Like(log.Original!, $"%{request.Keywords}%"))
                || (log.OriginalTitle != null && EF.Functions.Like(log.OriginalTitle!, $"%{request.Keywords}%"))
                || EF.Functions.Like(log.Description!, $"%{request.Keywords}%")
                || EF.Functions.Like(log.Content!, $"%{request.Keywords}%"));
        }

        int total = await query.CountAsync();
        IIncludableQueryable<BlogPost, List<BlogPostTag>?> datasFromDb =
            query.OrderByDescending(x => x.CreationTime)
                .Skip((request.Current - 1) * request.PageSize)
                .Take(request.PageSize)
                .Include(blogPost => blogPost.Albums)
                .Include(blogPost => blogPost.Categories)
                .Include(blogPost => blogPost.Tags);
        List<BlogPostBrief> data = await datasFromDb.Select(x => new BlogPostBrief(
            x.Title,
            x.Slug,
            x.Description,
            x.Original,
            (from blogPostCategory in x.Categories
                join category in _dbContext.Categories! on blogPostCategory.CategoryId equals category.Id
                select new CategoryBrief(category.Slug, category.Name, category.Description, 0)).ToList(),
            x.CreationTime,
            x.ViewCount)).ToListAsync();
        return new GetBlogPostBriefListResponse(data, total, true, request.PageSize, request.Current);
    }

    public async Task<GetBlogPostBriefListByCategorySlugResponse> GetBlogPostBriefListByCategorySlugAsync(
        GetBlogPostBriefListByCategorySlugRequest request)
    {
        IQueryable<BlogPost> query = _dbContext.BlogPosts!.AsQueryable();
        Category? category = await _dbContext.Categories!.FirstOrDefaultAsync(x => x.Slug == request.Slug);
        if (category == null)
        {
            return new GetBlogPostBriefListByCategorySlugResponse(null, null, 0, false, request.PageSize,
                request.Current);
        }

        IQueryable<BlogPost> datasFromDb =
            query.OrderByDescending(x => x.CreationTime)
                .Include(blogPost => blogPost.Albums)
                .Include(blogPost => blogPost.Categories)
                .Include(blogPost => blogPost.Tags)
                .Where(x => x.Categories != null && x.Categories.Any(y => y.CategoryId == category.Id) == true);

        int total = await datasFromDb.CountAsync();

        List<BlogPostBrief> data = await datasFromDb.Skip((request.Current - 1) * request.PageSize)
            .Take(request.PageSize).Select(x =>
                new BlogPostBrief(
                    x.Title,
                    x.Slug,
                    x.Description,
                    x.Original,
                    (from blogPostCategory in x.Categories
                        join cat in _dbContext.Categories! on blogPostCategory.CategoryId equals cat.Id
                        select new CategoryBrief(cat.Slug, cat.Name, cat.Description, 0)).ToList(),
                    x.CreationTime,
                    x.ViewCount)).ToListAsync();
        return new GetBlogPostBriefListByCategorySlugResponse(category.Name, data, total, true, request.PageSize,
            request.Current);
    }

    public async Task<BlogPostDetails?> GetBlogPostDetailsBySlugAsync(string slug)
    {
        BlogPost? blogPost = await _dbContext.BlogPosts!.Include(x => x.Albums)
            .Include(x => x.Categories)
            .Include(x => x.Tags)
            .FirstOrDefaultAsync(x => x.Slug == slug);
        if (blogPost == null)
        {
            return null;
        }

        return new BlogPostDetails(
            blogPost.Title,
            blogPost.Slug,
            blogPost.Description,
            blogPost.Content,
            blogPost.Original,
            (from blogPostCategory in blogPost.Categories
                join category in _dbContext.Categories! on blogPostCategory.CategoryId equals category.Id
                select new CategoryBrief(category.Slug, category.Name, category.Description)).ToList(),
            blogPost.CreationTime,
            blogPost.ViewCount);
    }

    public async Task<bool> IncreaseViewCountAsync(string slug)
    {
        BlogPost? blogPost = await _dbContext.BlogPosts!.FirstOrDefaultAsync(x => x.Slug == slug);
        if (blogPost == null)
        {
            return false;
        }

        blogPost.IncreaseViewCount();
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<List<BlogPostArchiveItem>?> GetArchivesAsync()
    {
        return await _dbContext.BlogPosts!.Select(x => new BlogPostArchiveItem(x.Title, x.Slug, x.CreationTime))
            .ToListAsync();
    }
}