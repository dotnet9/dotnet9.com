using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Dotnet9.Albums;
using Dotnet9.Categories;
using Dotnet9.EntityFrameworkCore;
using Dotnet9.Tags;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dotnet9.Blogs;

public class EfCoreBlogPostRepository : EfCoreRepository<Dotnet9DbContext, BlogPost, Guid>, IBlogPostRepository
{
    public EfCoreBlogPostRepository(IDbContextProvider<Dotnet9DbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<BlogPostWithDetails> FindByTitleAsync(string title)
    {
        var query = await ApplyFilterAsync();

        return await query
            .Where(x => x.Title == title)
            .FirstOrDefaultAsync();
    }

    public async Task<BlogPostWithDetails> FindBySlugAsync([NotNull] string slug)
    {
        var query = await ApplyFilterAsync();

        return await query
            .Where(x => x.Slug == slug)
            .FirstOrDefaultAsync();
    }

    public async Task<List<BlogPostWithDetails>> GetListAsync(int skipCount, int maxResultCount, string sorting,
        [CanBeNull] string filter, [CanBeNull] string album, [CanBeNull] string category, [CanBeNull] string tag)
    {
        var dbSet = await ApplyFilterAsync();
        //var a=  dbSet.Take(5);

        return await dbSet
            .WhereIf(
                !album.IsNullOrWhiteSpace(),
                x => x.AlbumNames.Contains(album))
            .WhereIf(
                !category.IsNullOrWhiteSpace(),
                x => x.CategoryNames.Contains(category))
            .WhereIf(
                !tag.IsNullOrWhiteSpace(),
                x => x.TagNames.Contains(tag))
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                blogPost => blogPost.Title.Contains(filter)
                            || blogPost.Slug.Contains(filter) ||
                            blogPost.Content.Contains(filter)
            )
            .OrderBy(!sorting.IsNullOrWhiteSpace() ? sorting : nameof(BlogPost.Title))
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }


    public async Task<int> GetCountAsync([CanBeNull] string filter, [CanBeNull] string album, [CanBeNull] string category,
        [CanBeNull] string tag)
    {
        var dbSet = await ApplyFilterAsync();

        return await dbSet
            .WhereIf(
                !album.IsNullOrWhiteSpace(),
                x => x.AlbumNames.Contains(album))
            .WhereIf(
                !category.IsNullOrWhiteSpace(),
                x => x.CategoryNames.Contains(category))
            .WhereIf(
                !tag.IsNullOrWhiteSpace(),
                x => x.TagNames.Contains(tag))
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                blogPost => blogPost.Title.Contains(filter)
                            || blogPost.Slug.Contains(filter) ||
                            blogPost.Content.Contains(filter)
            )
            .CountAsync();
    }

    private async Task<IQueryable<BlogPostWithDetails>> ApplyFilterAsync()
    {
        var dbContext = await GetDbContextAsync();

        return (await GetDbSetAsync())
            .Include(x => x.Albums)
            .Include(x => x.Categories)
            .Include(x => x.Tags)
            .Select(x => new BlogPostWithDetails
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                ShortDescription = x.ShortDescription,
                Content = x.Content,
                CoverImageUrl = x.CoverImageUrl,
                CopyrightType = x.CopyrightType,
                Original = x.Original,
                OriginalTitle = x.OriginalTitle,
                OriginalLink = x.OriginalLink,
                AlbumNames = (from blogPostAlbum in x.Albums
                    join album in dbContext.Set<Album>() on blogPostAlbum.AlbumId equals album.Id
                    select album.Name).ToArray(),
                CategoryNames = (from blogPostCategory in x.Categories
                    join category in dbContext.Set<Category>() on blogPostCategory.CategoryId equals category.Id
                    select category.Name).ToArray(),
                TagNames = (from blogPostTag in x.Tags
                    join tag in dbContext.Set<Tag>() on blogPostTag.TagId equals tag.Id
                    select tag.Name).ToArray(),
                CreationTime = x.CreationTime
            }).AsNoTracking();
    }

    public override Task<IQueryable<BlogPost>> WithDetailsAsync()
    {
        return base.WithDetailsAsync(x => x.Albums, x => x.Categories, x => x.Tags);
    }
}