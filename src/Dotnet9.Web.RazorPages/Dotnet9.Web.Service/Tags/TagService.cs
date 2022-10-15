namespace Dotnet9.Web.Service.Tags;

internal class TagService : ITagService
{
    private readonly Dotnet9DbContext _dbContext;

    public TagService(Dotnet9DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TagBrief>> GetTagsAsync()
    {
        List<TagBrief> tags = await _dbContext.Tags!.Select(c => new TagBrief(c.Name,
                _dbContext.Set<BlogPostTag>().Count(d => d.TagId == c.Id)))
            .ToListAsync();
        IOrderedEnumerable<TagBrief> distinctTags = from tag in tags
            where tag.BlogCount > 0
            orderby tag.BlogCount descending
            select tag;
        return distinctTags.ToList();
    }
}