using Dotnet9.Domain.Albums;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Categories;
using Dotnet9.Domain.Tags;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.Blogs;

public class EfCoreBlogPostRepository : EfCoreRepository<BlogPost>, IBlogPostRepository
{
    public EfCoreBlogPostRepository(Dotnet9DbContext context) : base(context)
    {
    }

    public async Task<BlogPostWithDetails?> FindByTitleAsync(string title)
    {
        var query = await ApplyFilterAsync();

        return await query
            .Where(x => x.Title == title)
            .FirstOrDefaultAsync();
    }

    public async Task<BlogPostWithDetails?> FindBySlugAsync(string slug)
    {
        var query = await ApplyFilterAsync();

        return await query
            .Where(x => x.Slug == slug)
            .FirstOrDefaultAsync();
    }

    public async Task<List<BlogPostWithDetails>?> GetBlogPostListByAlbumSlugAsync(string albumSlug)
    {
        var dbContext = await GetDbContextAsync();
        var album = await dbContext.Albums!.FirstOrDefaultAsync(x => x.Slug == albumSlug);
        if (album == null) return null;

        return await dbContext.BlogPosts!
            .Include(x => x.Albums)
            .Include(x => x.Categories)
            .Include(x => x.Tags)
            .Where(x => x.Albums != null && x.Albums.Any(d => d.AlbumId == album.Id))
            .Select(x => new BlogPostWithDetails
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                BriefDescription = x.BriefDescription,
                Content = x.Content,
                Cover = x.Cover,
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
                CreateDate = x.CreateDate
            }).ToListAsync();
    }

    public async Task<List<BlogPostWithDetails>?> GetBlogPostListByCategorySlugAsync(string categorySlug)
    {
        var dbContext = await GetDbContextAsync();
        var category = await dbContext.Categories!.FirstOrDefaultAsync(x => x.Slug == categorySlug);
        if (category == null) return null;

        return await dbContext.BlogPosts!
            .Include(x => x.Albums)
            .Include(x => x.Categories)
            .Include(x => x.Tags)
            .Where(x => x.Categories != null && x.Categories.Any(d => d.CategoryId == category.Id))
            .Select(x => new BlogPostWithDetails
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                BriefDescription = x.BriefDescription,
                Content = x.Content,
                Cover = x.Cover,
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
                CreateDate = x.CreateDate
            }).ToListAsync();
    }

    private async Task<IQueryable<BlogPostWithDetails>> ApplyFilterAsync()
    {
        var dbContext = await GetDbContextAsync();

        return DbContext.BlogPosts!
            .Include(x => x.Albums)
            .Include(x => x.Categories)
            .Include(x => x.Tags)
            .Select(x => new BlogPostWithDetails
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                BriefDescription = x.BriefDescription,
                Content = x.Content,
                Cover = x.Cover,
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
                CreateDate = x.CreateDate
            }).AsNoTracking();
    }
}