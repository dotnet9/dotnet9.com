namespace Dotnet9.Application.Blog;

/// <summary>
/// 文章所属栏目管理
/// </summary>
public class ArticleCategoryService : ITransient
{
    private readonly ISqlSugarRepository<ArticleCategory> _repository;

    public ArticleCategoryService(ISqlSugarRepository<ArticleCategory> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 添加文章所属栏目
    /// </summary>
    /// <param name="articleId">文章ID</param>
    /// <param name="categoryId">栏目ID</param>
    /// <returns></returns>
    public async Task Add(long articleId, long categoryId)
    {
        await _repository.InsertAsync(new ArticleCategory()
        {
            ArticleId = articleId,
            CategoryId = categoryId
        });
    }

    /// <summary>
    /// 更新文章所属栏目
    /// </summary>
    /// <param name="articleId">文章ID</param>
    /// <param name="categoryId">栏目ID</param>
    /// <returns></returns>
    public async Task Update(long articleId, long categoryId)
    {
        await _repository.UpdateAsync(x => new ArticleCategory()
        {
            CategoryId = categoryId
        }, x => x.ArticleId == articleId);
    }
}