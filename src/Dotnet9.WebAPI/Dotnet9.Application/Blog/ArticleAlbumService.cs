namespace Dotnet9.Application.Blog;

/// <summary>
/// 文章所属专辑管理
/// </summary>
public class ArticleAlbumService : ITransient
{
    private readonly ISqlSugarRepository<ArticleAlbum> _repository;

    public ArticleAlbumService(ISqlSugarRepository<ArticleAlbum> repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 添加文章所属专辑
    /// </summary>
    /// <param name="articleId">文章ID</param>
    /// <param name="albumId">专辑ID</param>
    /// <returns></returns>
    public async Task Add(long articleId, long albumId)
    {
        await _repository.InsertAsync(new ArticleAlbum()
        {
            ArticleId = articleId,
            AlbumId = albumId
        });
    }

    /// <summary>
    /// 更新文章所属专辑
    /// </summary>
    /// <param name="articleId">文章ID</param>
    /// <param name="albumId">专辑ID</param>
    /// <returns></returns>
    public async Task Update(long articleId, long albumId)
    {
        await _repository.UpdateAsync(x => new ArticleAlbum()
        {
            AlbumId = albumId
        }, x => x.ArticleId == articleId);
    }
}