using Dotnet9.Application.Blog.Dtos;

namespace Dotnet9.Application.Blog;
/// <summary>
/// 相册管理
/// </summary>
public class AlbumsService : BaseService<Albums>
{
    private readonly ISqlSugarRepository<Albums> _repository;

    public AlbumsService(ISqlSugarRepository<Albums> repository) : base(repository)
    {
        _repository = repository;
    }

    [DisplayName("相册列表分页查询")]
    [HttpGet]
    public async Task<PageResult<AlbumsPageOutput>> Page([FromQuery] AlbumsPageQueryInput dto)
    {
        return await _repository.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(dto.Name), x => x.Name.Contains(dto.Name))
            .WhereIF(dto.Type.HasValue, x => x.Type == dto.Type)
            .OrderBy(x => x.Sort)
            .Select(x => new AlbumsPageOutput
            {
                Id = x.Id,
                Name = x.Name,
                Type = x.Type,
                Status = x.Status,
                IsVisible = x.IsVisible,
                Sort = x.Sort,
                Remark = x.Remark,
                Cover = x.Cover,
                CreatedTime = x.CreatedTime
            }).ToPagedListAsync(dto);
    }

    /// <summary>
    /// 新增相册
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("新增相册")]
    [HttpPost("add")]
    public async Task Add(AddAlbumsInput dto)
    {
        var albums = dto.Adapt<Albums>();
        await _repository.InsertAsync(albums);
    }

    /// <summary>
    /// 更新相册
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("更新相册信息")]
    [HttpPut("edit")]
    public async Task Update(UpdateAlbumsInput dto)
    {
        var albums = await _repository.GetByIdAsync(dto.Id);
        if (albums == null) throw Oops.Bah("无效参数");
        dto.Adapt(albums);
        await _repository.UpdateAsync(albums);
    }


}