using Dotnet9.Application.Blog.Dtos;

namespace Dotnet9.Application.Blog;

/// <summary>
/// 文章管理
/// </summary>
public class ArticleService : BaseService<Article>
{
    private readonly ISqlSugarRepository<Article> _repository;
    private readonly IIdGenerator _idGenerator;

    public ArticleService(ISqlSugarRepository<Article> repository,
        IIdGenerator idGenerator) : base(repository)
    {
        _repository = repository;
        _idGenerator = idGenerator;
    }

    /// <summary>
    /// 文章列表分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("【后端】文章列表分页查询")]
    [HttpGet]
    public async Task<PageResult<ArticlePageOutput>> Page([FromQuery] ArticlePageQueryInput dto)
    {
        List<long> categoryList = new();
        if (dto.CategoryId.HasValue)
        {
            var list = await _repository.AsSugarClient().Queryable<Categories>()
                .Where(x => x.Status == AvailabilityStatus.Enable)
                .ToChildListAsync(x => x.ParentId, dto.CategoryId);
            categoryList = list.Select(x => x.Id).ToList();
            categoryList.Add(dto.CategoryId.Value);
        }

        return await _repository.AsQueryable().LeftJoin<ArticleCategory>((article, ac) => article.Id == ac.ArticleId)
            .InnerJoin<Categories>((article, ac, c) => ac.CategoryId == c.Id && c.Status == AvailabilityStatus.Enable)
            .WhereIF(!string.IsNullOrWhiteSpace(dto.Title),
                article => article.Title.Contains(dto.Title) || article.Summary.Contains(dto.Title) ||
                           article.Content.Contains(dto.Title))
            .WhereIF(categoryList.Any(), (article, ac) => categoryList.Contains(ac.CategoryId))
            .OrderByDescending(article => article.IsTop)
            .OrderBy(article => article.Sort)
            .OrderByDescending(article => article.PublishTime)
            .Select((article, ac, c) => new ArticlePageOutput
            {
                Id = article.Id,
                Title = article.Title,
                Slug = article.Slug,
                Status = article.Status,
                Sort = article.Sort,
                Cover = article.Cover,
                IsTop = article.IsTop,
                CreatedTime = article.CreatedTime,
                CreationType = article.CreationType,
                PublishTime = article.PublishTime,
                Views = article.Views,
                CategoryName = c.Name
            }).ToPagedListAsync(dto);
    }

    /// <summary>
    /// 添加文章
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("添加文章")]
    [HttpPost("add")]
    [UnitOfWork]
    public async Task Add(AddArticleInput dto)
    {
        var article = dto.Adapt<Article>();
        article.Id = _idGenerator.NewLong();
        article.ShortSlug = article.Slug.Encode();
        var tags = dto.Tags?.Select(x => new ArticleTag()
        {
            ArticleId = article.Id,
            TagId = x
        }).ToList();
        await _repository.InsertAsync(article);
        await _repository.AsSugarClient().Insertable(tags).ExecuteCommandAsync();
        var articleCategory = new ArticleCategory()
        {
            ArticleId = article.Id,
            CategoryId = dto.CategoryId
        };
        await _repository.AsSugarClient().Insertable(articleCategory).ExecuteCommandAsync();
        var articleAlbum = new ArticleAlbum()
        {
            ArticleId = article.Id,
            AlbumId = dto.AlbumId
        };
        await _repository.AsSugarClient().Insertable(articleAlbum).ExecuteCommandAsync();
    }

    /// <summary>
    /// 更新文章
    /// </summary>
    /// <returns></returns>
    [DisplayName("更新文章")]
    [HttpPut("edit")]
    [UnitOfWork]
    public async Task Update(UpdateArticleInput dto)
    {
        var article = await _repository.GetByIdAsync(dto.Id);
        if (article == null) throw Oops.Oh("无效参数");
        dto.Adapt(article);
        await _repository.UpdateAsync(article);
        await _repository.AsSugarClient().Deleteable<ArticleTag>()
            .Where(x => x.ArticleId == dto.Id)
            .ExecuteCommandHasChangeAsync();
        var tags = dto.Tags.Select(x => new ArticleTag()
        {
            ArticleId = article.Id,
            TagId = x
        }).ToList();
        await _repository.AsSugarClient()
            .Insertable(tags)
            .ExecuteCommandAsync();
        await _repository.AsSugarClient()
            .Updateable<ArticleCategory>()
            .SetColumns(x => x.CategoryId == dto.CategoryId)
            .Where(x => x.ArticleId == dto.Id)
            .ExecuteCommandHasChangeAsync();
        await _repository.AsSugarClient()
            .Updateable<ArticleAlbum>()
            .SetColumns(x => x.AlbumId == dto.AlbumId)
            .Where(x => x.ArticleId == dto.Id)
            .ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 文章详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [DisplayName("文章详情")]
    [HttpGet]
    public async Task<ArticleDetailOutput> Detail([FromQuery] long id)
    {
        return await _repository.AsQueryable().LeftJoin<ArticleCategory>((article, ac) => article.Id == ac.ArticleId)
            .InnerJoin<Categories>((article, ac, c) => ac.CategoryId == c.Id && c.Status == AvailabilityStatus.Enable)
            .LeftJoin<ArticleAlbum>((article, ac, c, aa) => article.Id == aa.ArticleId)
            .LeftJoin<Albums>((article, ac, c, aa, a) => aa.AlbumId == a.Id)
            .Where(article => article.Id == id)
            .Select((article, ac, c, aa, a) => new ArticleDetailOutput
            {
                Id = article.Id,
                Title = article.Title,
                Slug = article.Slug,
                Summary = article.Summary,
                Cover = article.Cover,
                Status = article.Status,
                Link = article.Link,
                IsTop = article.IsTop,
                Sort = article.Sort,
                Author = article.Author,
                Content = article.Content,
                IsAllowComments = article.IsAllowComments,
                IsHtml = article.IsHtml,
                CreationType = article.CreationType,
                CategoryId = c.Id,
                AlbumId = a.Id,
                ExpiredTime = article.ExpiredTime,
                PublishTime = article.PublishTime,
                Tags = SqlFunc.Subqueryable<Tags>().InnerJoin<ArticleTag>((tags, at) => tags.Id == at.TagId)
                    .Where((tags, at) => at.ArticleId == article.Id && tags.Status == AvailabilityStatus.Enable)
                    .ToList(tags => tags.Id)
            }).FirstAsync();
    }
}