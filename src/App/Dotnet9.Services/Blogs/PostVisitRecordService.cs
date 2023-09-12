using Dotnet9.Models.Dtos.DashBoard;
using Dotnet9.Repositoies.Blogs;

namespace Dotnet9.Services.Blogs;

/// <summary>
///     文章访问记录
/// </summary>
public class PostVisitRecordService
{
    private readonly PostVisitRecordRepository _postVisitRecord;

    public PostVisitRecordService(PostVisitRecordRepository postVisitRecord)
    {
        _postVisitRecord = postVisitRecord;
    }

    public async Task<PageDto<VisitLogModel>> GetVisitList(int pageIndex, int pageSize)
    {
        PageDto<PostVisitRecord> res = await _postVisitRecord.GetListAsync(a => true, pageIndex, pageSize);
        List<VisitLogModel> list = res.Data.Select(a => new VisitLogModel()).ToList();
        return new PageDto<VisitLogModel>(res.Total, list);
    }
}