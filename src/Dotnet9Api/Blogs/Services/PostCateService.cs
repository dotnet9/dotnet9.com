using Dotnet9Tools.Exceptions;

namespace Dotnet9Api.Blogs.Services;

public class PostCateService
{
    private readonly Dotnet9DbContext _context;

    public PostCateService(Dotnet9DbContext context)
    {
        _context = context;
    }

    public async Task<PageDto<CateDtoModel>> GetList(BasePageModel model)
    {
        DbSet<PostCates> query = _context.PostCates;
        List<CateDtoModel> list = await query.Skip(model.Skip).Take(model.PageSize)
            .Include(a => a.PostCateRelations)
            .Select(a => new CateDtoModel
            {
                Id = a.Id,
                CateName = a.CateName,
                CreateTime = a.CreateTime,
                PostCount = a.PostCateRelations.Count()
            }).ToListAsync();
        return new PageDto<CateDtoModel>(await query.CountAsync(), list);
    }

    public async Task Edit(CateRequest request)
    {
        if (request.Id != null)
        {
            PostCates? item = await _context.PostCates.FirstOrDefaultAsync(a => a.Id == request.Id);
            if (item == null)
            {
                throw new UserException("找不到更新的分类");
            }

            item.CateName = request.CateName;
            await _context.SaveChangesAsync();
        }
        else
        {
            await _context.PostCates.AddAsync(new PostCates(request.CateName));
            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid Id)
    {
        PostCates? item = await _context.PostCates.Include(a => a.PostCateRelations).Where(a => a.Id == Id)
            .FirstOrDefaultAsync();
        if (item is { PostCateRelations.Count: > 0 })
        {
            throw new UserException("分类下不为空，不能删除");
        }

        await _context.PostCates.Where(a => a.Id == Id).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }
}