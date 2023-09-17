using Dotnet9.Models.Dtos.Blogs.Posts;
using Dotnet9Tools.Helper;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace Dotnet9.Repositoies.Blogs;

public class PostRepository : BaseRepository<Posts, Guid>
{
    public PostRepository(DbContext dbContext) : base(dbContext)
    {
    }

    public async Task<PostDetailModel?> GetById(Guid id)
    {
        return await GetDetails(post => post.Id == id);
    }

    public async Task<PostDetailModel?> GetBySlug(string slug)
    {
        return await GetDetails(post => post.Slug == slug);
    }

    public async Task<PostDetailModel?> GetByShortId(string shortId)
    {
        return await GetDetails(post => post.ShortId == shortId);
    }

    private async Task<PostDetailModel?> GetDetails(Expression<Func<Posts, bool>> predicate)
    {
        PostDetailModel? item = await Ctx.Set<Posts>().AsNoTracking()
            .Include(a => a.CateRelations)
            .Include(a => a.TagRelations)
            .Where(predicate).Select(a => new PostDetailModel
            {
                Id = a.Id,
                Title = a.Title,
                Content = a.Content,
                Thumb = a.Thumb,
                Snippet = a.Snippet,
                Cates = a.CateRelations.Select(b => new PostCateModel
                        { Id = b.PostCate.Id, CateName = b.PostCate.CateName })
                    .ToList(),
                Tags = a.TagRelations.Select(b => new PostTagModel
                {
                    Id = b.PostTags.Id,
                    TagName = b.PostTags.TagName
                }).ToList(),
                CreateTime = a.CreateTime,
                UpdateTime = a.UpdateTime,
                ReadCount = a.ReadCount,
                IsPublish = a.IsPublish
            }).FirstOrDefaultAsync();
        return item;
    }

    public async Task<PageDto<PostItemModel>> GetHomeList(PostRequestModel request)
    {
        IQueryable<Posts> query = Ctx.Set<Posts>().AsQueryable();
        if (!request.Keywords.IsNullOrEmpty())
        {
            var keywords = $"%{request.Keywords}%";
            query = query.Where(post =>
                EF.Functions.Like(post.Title, keywords)
                || EF.Functions.Like(post.Slug, keywords)
                || EF.Functions.Like(post.Content, keywords));
        }

        int count = await query.CountAsync();
        List<PostItemModel> list = await query
            .WhereIf(request.FilterPublish, a => a.IsPublish == request.PublishStatus)
            .Include(a => a.TagRelations)
            .Include(a => a.PostComments)
            .Include(a => a.CateRelations)
            .WhereIf(request.TagId.HasValue, a => a.TagRelations.Any(x => x.PostTags.Id == request.TagId))
            .WhereIf(request.CateId.HasValue, a => a.CateRelations.Any(x => x.PostCate.Id == request.CateId))
            .Skip(request.Skip).Take(request.PageSize)
            .OrderByDescending(a => a.IsTop)
            .ThenByDescending(a => a.CreateTime)
            .ThenByDescending(a => a.UpdateTime)
            .Select(a => new PostItemModel
            {
                Id = a.Id,
                Title = a.Title,
                Slug = a.Slug,
                ShortId = a.ShortId,
                Content = a.Content,
                CreateTime = a.CreateTime,
                LastUpdateTime = a.UpdateTime,
                Snippet = a.Snippet,
                CateItems = a.CateRelations.Select(x => new CateItem
                {
                    Id = x.PostCate.Id,
                    CateName = x.PostCate.CateName
                }).ToList(),
                TagItems = a.TagRelations.Select(x => new TagItem
                {
                    Id = x.PostTags.Id,
                    TagName = x.PostTags.TagName
                }).ToList(),
                ReadCount = a.ReadCount,
                CommentCount = a.PostComments.Count(),
                IsTop = a.IsTop,
                IsPublish = a.IsPublish,
                Thumb = a.Thumb
            }).ToListAsync();
        return new PageDto<PostItemModel>(count, list);
    }


    public async Task<PostEditRequest> Get(Guid Id)
    {
        PostEditRequest? item = await Ctx.Set<Posts>().Include(a => a.CateRelations).Where(a => a.Id == Id).Select(a =>
            new PostEditRequest
            {
                Id = a.Id,
                Cates = a.CateRelations.Select(x => x.PostCate.Id).ToList(),
                Title = a.Title,
                Content = a.Content,
                Snippet = a.Snippet,
                Thumb = a.Thumb,
                IsPublish = a.IsPublish,
                Tags = a.TagRelations.Select(x => x.PostTags.TagName).ToList()
            }).FirstOrDefaultAsync();
        if (item == null)
        {
            throw new UserException("未找到文章");
        }

        item.TagListToStr();
        return item;
    }


    /// <summary>
    ///     删除
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public async Task Delete(Guid Id)
    {
        Posts? post = await FindByIdAsync(Id);
        if (post != null)
        {
            await DeleteAsync(post);
        }
    }

    /// <summary>
    ///     置顶
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    public async Task Top(Guid Id)
    {
        Posts? item = await Ctx.Set<Posts>().FirstOrDefaultAsync(a => a.Id == Id);
        if (item != null)
        {
            item.IsTop = !item.IsTop;
            await UpdateAsync(item);
        }
    }

    /// <summary>
    ///     发布
    /// </summary>
    /// <param name="Id"></param>
    public async Task Publish(Guid Id)
    {
        Posts? item = await Ctx.Set<Posts>().FirstOrDefaultAsync(a => a.Id == Id);
        if (item != null)
        {
            item.IsPublish = !item.IsPublish;
            await UpdateAsync(item);
        }
    }
}