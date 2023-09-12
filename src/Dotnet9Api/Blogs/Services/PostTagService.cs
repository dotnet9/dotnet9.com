namespace Dotnet9Api.Blogs.Services;

public class PostTagService
{
    private readonly Dotnet9DbContext _context;

    public PostTagService(Dotnet9DbContext context)
    {
        _context = context;
    }

    public async Task<PageDto<TagDtoModel>> GetList(TagListRequest request)
    {
        IQueryable<PostTags> query = _context.PostTags.AsQueryable();
        List<TagDtoModel> list = await query.Skip(request.Skip).Take(request.PageSize)
            .Include(a => a.TagRelation)
            .Select(a => new TagDtoModel
            {
                Id = a.Id,
                TagName = a.TagName,
                Count = a.TagRelation.Count()
            }).ToListAsync();
        return new PageDto<TagDtoModel>(await query.CountAsync(), list);
    }
}