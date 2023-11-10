using Dotnet9.Application.Auth;
using Dotnet9.Application.Client.Dtos;
using Furion.UnifyResult;

namespace Dotnet9.Application.Client;

/// <summary>
/// 博客文章
/// </summary>
[ApiDescriptionSettings("博客前端接口")]
[AllowAnonymous]
public class ArticleController : IDynamicApiController
{
    private readonly IEasyCachingProvider _easyCachingProvider;
    private readonly ISqlSugarRepository<Tags> _tagsRepository;
    private readonly ISqlSugarRepository<Article> _articleRepository;
    private readonly AuthManager _authManager;
    private readonly ISqlSugarRepository<Categories> _categoryRepository;
    private readonly ISqlSugarRepository<Albums> _albumRepository;
    private readonly ISqlSugarRepository<AuthAccount> _authAccountRepository;
    private readonly ISqlSugarRepository<FriendLink> _friendLinkRepository;

    public ArticleController(IEasyCachingProvider easyCachingProvider,
        ISqlSugarRepository<Tags> tagsRepository,
        ISqlSugarRepository<Article> articleRepository,
        AuthManager authManager,
        ISqlSugarRepository<Categories> categoryRepository,
        ISqlSugarRepository<Albums> albumRepository,
        ISqlSugarRepository<AuthAccount> authAccountRepository,
        ISqlSugarRepository<FriendLink> friendLinkRepository)
    {
        _easyCachingProvider = easyCachingProvider;
        _tagsRepository = tagsRepository;
        _articleRepository = articleRepository;
        _authManager = authManager;
        _categoryRepository = categoryRepository;
        _albumRepository = albumRepository;
        _authAccountRepository = authAccountRepository;
        _friendLinkRepository = friendLinkRepository;
    }

    /// <summary>
    /// 文章表查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PageResult<ArticleOutput>> Get([FromQuery] ArticleListQueryInput dto)
    {
        Tags? tag = null;
        if (!string.IsNullOrWhiteSpace(dto.TagName))
        {
            tag = await _tagsRepository.AsQueryable()
                .Where(x => x.Name == dto.TagName && x.Status == AvailabilityStatus.Enable)
                .FirstAsync();
            if (tag == null)
            {
                throw Oops.Bah($"不存在标签名：{dto.TagName}").StatusCode(404);
            }

            UnifyContext.Fill(new { tag.Name, tag.Cover });
        }

        Categories? category = null;
        if (!string.IsNullOrWhiteSpace(dto.CategorySlug))
        {
            category = await _categoryRepository.AsQueryable()
                .Where(x => x.Slug == dto.CategorySlug && x.Status == AvailabilityStatus.Enable).FirstAsync();
            if (category == null)
            {
                throw Oops.Bah($"不存在分类别名：{dto.CategorySlug}").StatusCode(404);
            }

            UnifyContext.Fill(new { category.Name, category.Cover });
        }

        Albums? album = null;
        if (!string.IsNullOrWhiteSpace(dto.AlbumSlug))
        {
            album = await _albumRepository.AsQueryable()
                .Where(x => x.Slug == dto.AlbumSlug && x.Status == AvailabilityStatus.Enable).FirstAsync();
            if (album == null)
            {
                throw Oops.Bah($"不存在专辑别名：{dto.AlbumSlug}").StatusCode(404);
            }

            UnifyContext.Fill(new { album.Name, album.Cover });
        }

        return await _articleRepository.AsQueryable()
            .LeftJoin<ArticleCategory>((article, ac) => article.Id == ac.ArticleId)
            .InnerJoin<Categories>((article, ac, c) => ac.CategoryId == c.Id && c.Status == AvailabilityStatus.Enable)
            .LeftJoin<ArticleAlbum>((article, ac, c, aa) => article.Id == aa.ArticleId)
            .LeftJoin<Albums>((article, ac, c, aa, a) => aa.AlbumId == a.Id)
            .Where(article => article.Status == AvailabilityStatus.Enable && article.PublishTime <= SqlFunc.GetDate())
            .Where(article => article.ExpiredTime == null || SqlFunc.GetDate() < article.ExpiredTime)
            .WhereIF(category != null, (article, ac, c, aa, a) => c.Slug == category.Slug)
            .WhereIF(album != null, (article, ac, c, aa, a) => a.Slug == album.Slug)
            .WhereIF(!string.IsNullOrWhiteSpace(dto.Keyword),
                article => article.Title.Contains(dto.Keyword) || article.Summary.Contains(dto.Keyword) ||
                           article.Content.Contains(dto.Keyword))
            .WhereIF(tag != null,
                article => SqlFunc.Subqueryable<Tags>().InnerJoin<ArticleTag>((tags, at) => tags.Id == at.TagId)
                    .Where((tags, at) => tags.Status == AvailabilityStatus.Enable && at.ArticleId == article.Id &&
                                         tags.Id == tag.Id).Any())
            .OrderByDescending(article => article.IsTop)
            .OrderBy(article => article.Sort)
            .OrderByDescending(article => article.PublishTime)
            .Select((article, ac, c, aa, a) => new ArticleOutput
            {
                Id = article.Id,
                Title = article.Title,
                Slug = article.Slug,
                ShortSlug = article.ShortSlug,
                CategoryId = c.Id,
                CategoryName = c.Name,
                CategorySlug = c.Slug,
                AlbumId = a.Id,
                AlbumName = a.Name,
                AlbumSlug = a.Slug,
                IsTop = article.IsTop,
                CreationType = article.CreationType,
                Summary = article.Summary,
                Cover = article.Cover,
                PublishTime = article.PublishTime,
                Tags = SqlFunc.Subqueryable<Tags>().InnerJoin<ArticleTag>((tags, at) => tags.Id == at.TagId)
                    .Where((tags, at) => tags.Status == AvailabilityStatus.Enable && at.ArticleId == article.Id)
                    .ToList(tags => new TagsOutput
                    {
                        Id = tags.Id,
                        Name = tags.Name,
                        Color = tags.Color,
                        Icon = tags.Icon
                    })
            }).ToPagedListAsync(dto);
    }

    /// <summary>
    /// 标签列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<TagsOutput>> Tags()
    {
        return await _tagsRepository.AsQueryable().Where(x => x.Status == AvailabilityStatus.Enable)
            .OrderBy(x => x.Sort)
            .Select(x => new TagsOutput
            {
                Id = x.Id,
                Icon = x.Icon,
                Name = x.Name,
                Color = x.Color
            }).ToListAsync();
    }

    /// <summary>
    /// 文章分类
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<CategoryOutput>> Categories()
    {
        var queryable = _articleRepository.AsQueryable().Where(a =>
            a.Status == AvailabilityStatus.Enable && a.PublishTime <= SqlFunc.GetDate() &&
            (a.ExpiredTime == null || SqlFunc.GetDate() < a.ExpiredTime));
        return await _categoryRepository.AsQueryable().LeftJoin<ArticleCategory>((c, ac) => c.Id == ac.CategoryId)
            .LeftJoin(queryable, (c, ac, a) => ac.ArticleId == a.Id)
            .Where(c => c.Status == AvailabilityStatus.Enable)
            .WithCache()
            .GroupBy(c => new { c.Id, c.ParentId, c.Name, c.Sort })
            .Select((c, ac, a) => new CategoryOutput
            {
                Id = c.Id,
                ParentId = c.ParentId,
                Sort = c.Sort,
                Name = c.Name,
                Slug = c.Slug,
                Total = SqlFunc.AggregateCount(a.Id)
            })
            .MergeTable()
            .OrderBy(x => x.Sort)
            .OrderBy(x => x.ParentId)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    /// <summary>
    /// 标签列表
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<AlbumOutput>> Albums()
    {
        var queryable = _articleRepository.AsQueryable().Where(a =>
            a.Status == AvailabilityStatus.Enable && a.PublishTime <= SqlFunc.GetDate() &&
            (a.ExpiredTime == null || SqlFunc.GetDate() < a.ExpiredTime));
        return await _albumRepository.AsQueryable().LeftJoin<ArticleAlbum>((c, ac) => c.Id == ac.AlbumId)
            .LeftJoin(queryable, (c, ac, a) => ac.ArticleId == a.Id)
            .Where(c => c.Status == AvailabilityStatus.Enable)
            .WithCache()
            .GroupBy(c => new { c.Id, c.Name, c.Sort })
            .Select((c, ac, a) => new AlbumOutput
            {
                Id = c.Id,
                Sort = c.Sort,
                Name = c.Name,
                Slug = c.Slug,
                Total = SqlFunc.AggregateCount(a.Id)
            })
            .MergeTable()
            .OrderBy(x => x.Sort)
            .OrderBy(x => x.Id)
            .ToListAsync();
    }

    /// <summary>
    /// 文章信息统计
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ArticleReportOutput> Report()
    {
        //统计文章数量
        var articleCount = await _articleRepository.AsQueryable()
            .Where(x => x.Status == AvailabilityStatus.Enable &&
                        (x.ExpiredTime == null || SqlFunc.GetDate() < x.ExpiredTime))
            .Where(x => x.PublishTime <= SqlFunc.GetDate())
            .CountAsync();

        //标签统计
        var tagCount = await _tagsRepository.AsQueryable().Where(x => x.Status == AvailabilityStatus.Enable)
            .CountAsync();
        //分类统计
        var categoryCount = await _categoryRepository.AsQueryable().Where(x => x.Status == AvailabilityStatus.Enable)
            .CountAsync();
        //专辑统计
        var albumCount = await _albumRepository.AsQueryable().Where(x => x.Status == AvailabilityStatus.Enable)
            .CountAsync();

        var userCount = await _authAccountRepository.AsQueryable().CountAsync();

        var linkCount =
            await _friendLinkRepository.AsQueryable().CountAsync(x => x.Status == AvailabilityStatus.Enable);

        return new ArticleReportOutput
        {
            ArticleCount = articleCount,
            CategoryCount = categoryCount,
            AlbumCount = albumCount,
            TagCount = tagCount,
            LinkCount = linkCount,
            UserCount = userCount
        };
    }

    /// <summary>
    /// 文章详情
    /// </summary>
    /// <param name="slugOrShortSlug">文章别名或短别名</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ArticleInfoOutput> Info([FromQuery] string slugOrShortSlug)
    {
        var cacheBlogPost = await _easyCachingProvider.GetAsync<ArticleInfoOutput>(slugOrShortSlug);
        if (cacheBlogPost.HasValue)
        {
            return cacheBlogPost.Value;
        }

        long userId = _authManager.UserId;
        var article = await _articleRepository.AsQueryable()
            .LeftJoin<ArticleCategory>((x, ac) => x.Id == ac.ArticleId)
            .InnerJoin<Categories>((x, ac, c) => ac.CategoryId == c.Id && c.Status == AvailabilityStatus.Enable)
            .LeftJoin<ArticleAlbum>((x, ac, c, aa) => x.Id == aa.ArticleId)
            .LeftJoin<Albums>((x, ac, c, aa, a) => aa.AlbumId == a.Id)
            .Where(x => (x.Slug == slugOrShortSlug || x.ShortSlug == slugOrShortSlug) &&
                        x.PublishTime <= DateTime.Now &&
                        x.Status == AvailabilityStatus.Enable)
            .Where(x => x.ExpiredTime == null || x.ExpiredTime > DateTime.Now)
            .Select((x, ac, c, aa, a) => new ArticleInfoOutput
            {
                Id = x.Id,
                Title = x.Title,
                Slug = x.Slug,
                ShortSlug = x.ShortSlug,
                Content = x.Content,
                Summary = x.Summary,
                Cover = x.Cover,
                PublishTime = x.PublishTime,
                Author = x.Author,
                Views = x.Views,
                CreationType = x.CreationType,
                IsAllowComments = x.IsAllowComments,
                IsHtml = x.IsHtml,
                IsTop = x.IsTop,
                Link = x.Link,
                UpdatedTime = x.UpdatedTime,
                CategoryId = c.Id,
                CategoryName = c.Name,
                CategorySlug = c.Slug,
                AlbumId = a.Id,
                AlbumName = a.Name,
                AlbumSlug = a.Slug,
                PraiseTotal = SqlFunc.Subqueryable<Praise>().Where(p => p.ObjectId == x.Id).Count(),
                IsPraise = SqlFunc.Subqueryable<Praise>().Where(p => p.ObjectId == x.Id && p.AccountId == userId).Any(),
                Tags = SqlFunc.Subqueryable<Tags>().InnerJoin<ArticleTag>((tags, at) => tags.Id == at.TagId)
                    .Where((tags, at) => tags.Status == AvailabilityStatus.Enable && at.ArticleId == x.Id)
                    .ToList(tags => new TagsOutput
                    {
                        Id = tags.Id,
                        Name = tags.Name,
                        Color = tags.Color,
                        Icon = tags.Icon
                    })
            }).FirstAsync();
        if (article == null) throw Oops.Bah("糟糕，您访问的信息丢失了...").StatusCode(404);
        await _articleRepository.UpdateAsync(x => new Article()
        {
            Views = x.Views + 1
        }, x => x.Id == article.Id);
        //上一篇
        var prevQuery = await _articleRepository.AsQueryable().Where(x =>
                SqlFunc.LessThan(x.PublishTime, article.PublishTime)
                && SqlFunc.LessThan(x.PublishTime, DateTime.Now) &&
                x.Status == AvailabilityStatus.Enable)
            .Where(x => x.ExpiredTime == null || SqlFunc.GreaterThan(x.ExpiredTime, DateTime.Now))
            .OrderByDescending(x => x.PublishTime)
            .Select(x => new ArticleBasicsOutput
                { Id = x.Id, Cover = x.Cover, Title = x.Title, Slug = x.Slug, PublishTime = x.PublishTime, Type = 0 })
            .FirstAsync();
        //下一篇
        var nextQuery = await _articleRepository.AsQueryable().Where(x =>
                x.PublishTime > article.PublishTime && x.PublishTime <= DateTime.Now &&
                x.Status == AvailabilityStatus.Enable)
            .Where(x => x.ExpiredTime == null || x.ExpiredTime > DateTime.Now)
            .OrderBy(x => x.PublishTime)
            .Select(x => new ArticleBasicsOutput
                { Id = x.Id, Cover = x.Cover, Title = x.Title, Slug = x.Slug, PublishTime = x.PublishTime, Type = 1 })
            .FirstAsync();
        //随机6条
        var randomQuery = await _articleRepository.AsQueryable().Where(x => x.Id != article.Id)
            .Where(x => x.PublishTime <= DateTime.Now && x.Status == AvailabilityStatus.Enable)
            .Where(x => x.ExpiredTime == null || x.ExpiredTime > DateTime.Now)
            .OrderBy(x => SqlFunc.GetRandom())
            .Select(x => new ArticleBasicsOutput
                { Id = x.Id, Cover = x.Cover, Title = x.Title, Slug = x.Slug, PublishTime = x.PublishTime, Type = 2 })
            .Take(6).ToListAsync();

        article.Prev = prevQuery;
        article.Next = nextQuery;
        article.Random = randomQuery;
        article.Views++;
        await _easyCachingProvider.SetAsync(slugOrShortSlug, article, TimeSpan.FromMinutes(5));
        return article;
    }

    /// <summary>
    /// 最新文章
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<ArticleBasicsOutput>> Latest()
    {
        return await _articleRepository.AsQueryable()
            .Where(x => x.Status == AvailabilityStatus.Enable && x.PublishTime <= DateTime.Now)
            .Where(x => x.ExpiredTime == null || x.ExpiredTime > DateTime.Now)
            .Take(5)
            .OrderByDescending(x => x.Id)
            .Select<ArticleBasicsOutput>()
            .ToListAsync();
    }
}