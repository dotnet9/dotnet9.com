using System.Linq.Expressions;
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

    public async Task<BlogPostWithDetails?> GetAsync(Expression<Func<BlogPost, bool>> whereLambda)
    {
        var dbContext = await GetDbContextAsync();

        return await DbContext.BlogPosts!
            .Include(x => x.Albums)
            .Include(x => x.Categories)
            .Include(x => x.Tags)
            .Where(whereLambda)
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
            }).FirstOrDefaultAsync();
    }

    public async Task<List<BlogPostWithDetails>?> SelectAsync(Expression<Func<BlogPost, bool>> whereLambda)
    {
        var dbContext = await GetDbContextAsync();

        return await dbContext.BlogPosts!
            .Include(x => x.Albums)
            .Include(x => x.Categories)
            .Include(x => x.Tags)
            .Where(whereLambda)
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
}