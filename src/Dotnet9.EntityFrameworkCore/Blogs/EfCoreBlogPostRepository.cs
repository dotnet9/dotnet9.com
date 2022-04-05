using System.Linq.Expressions;
using Dotnet9.Domain.Albums;
using Dotnet9.Domain.Blogs;
using Dotnet9.Domain.Categories;
using Dotnet9.Domain.Repositories;
using Dotnet9.Domain.Tags;
using Dotnet9.EntityFrameworkCore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9.EntityFrameworkCore.Blogs;

public class EfCoreBlogPostRepository : EfCoreRepository<BlogPost>, IBlogPostRepository
{
    public EfCoreBlogPostRepository(Dotnet9DbContext context) : base(context)
    {
    }

    public async Task<BlogPostWithDetails?> GetBlogPostAsync(Expression<Func<BlogPost, bool>> whereLambda)
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

    public async Task<List<BlogPostWithDetails>?> SelectBlogPostAsync<S>(Expression<Func<BlogPost, bool>> whereLambda,
        Expression<Func<BlogPost, S>> orderByLambda,
        SortDirectionKind sortDirection)
    {
        var dbContext = await GetDbContextAsync();

        var query = dbContext.BlogPosts!
            .Include(x => x.Albums)
            .Include(x => x.Categories)
            .Include(x => x.Tags)
            .Where(whereLambda);

        query = sortDirection == SortDirectionKind.Ascending
            ? query.AsNoTracking().OrderBy(orderByLambda)
            : query.AsNoTracking().OrderByDescending(orderByLambda);

        return await query
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


    public async Task<Tuple<List<BlogPostWithDetails>, int>> SelectBlogPostAsync<S>(int pageSize, int pageIndex,
        Expression<Func<BlogPost, bool>> whereLambda,
        Expression<Func<BlogPost, S>> orderByLambda, SortDirectionKind sortDirection)
    {
        var total = await DbContext.Set<BlogPost>().Where(whereLambda).CountAsync();

        var query = DbContext.BlogPosts!
            .Include(x => x.Albums)
            .Include(x => x.Categories)
            .Include(x => x.Tags)
            .Where(whereLambda);

        query = sortDirection == SortDirectionKind.Ascending
            ? query.AsNoTracking().OrderBy(orderByLambda)
            : query.AsNoTracking().OrderByDescending(orderByLambda);

        var lst = await query.Skip(pageSize * (pageIndex - 1)).Take(pageSize)
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
                    join album in DbContext.Set<Album>() on blogPostAlbum.AlbumId equals album.Id
                    select album.Name).ToArray(),
                CategoryNames = (from blogPostCategory in x.Categories
                    join category in DbContext.Set<Category>() on blogPostCategory.CategoryId equals category.Id
                    select category.Name).ToArray(),
                TagNames = (from blogPostTag in x.Tags
                    join tag in DbContext.Set<Tag>() on blogPostTag.TagId equals tag.Id
                    select tag.Name).ToArray(),
                CreateDate = x.CreateDate
            }).ToListAsync();

        return new Tuple<List<BlogPostWithDetails>, int>(lst, total);
    }
}