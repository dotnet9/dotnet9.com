using Dotnet9.Application.Blog.Dtos;

namespace Dotnet9.Application.Blog;

/// <summary>
/// 文章专辑管理
/// </summary>
public class AlbumsService : BaseService<Albums>
{
    private readonly ISqlSugarRepository<Albums> _repository;

    public AlbumsService(ISqlSugarRepository<Albums> repository) : base(repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 文章专辑列表分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("文章专辑列表")]
    [HttpGet]
    public async Task<PageResult<AlbumsPageOutput>> Page([FromQuery] AlbumsPageQueryInput dto)
    {
        return await _repository.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(dto.Name), x => x.Name.Contains(dto.Name))
            .OrderBy(x => x.Sort)
            .OrderByDescending(x => x.Id)
            .Select(x => new AlbumsPageOutput
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Status = x.Status,
                Sort = x.Sort,
                Cover = x.Cover,
                CreatedTime = x.CreatedTime,
                Remark = x.Remark
            }).ToPagedListAsync(dto);
    }

    /// <summary>
    /// 添加文章专辑
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("添加文章专辑")]
    [HttpPost("add")]
    public async Task Add(AddAlbumInput dto)
    {
        var albums = dto.Adapt<Albums>();
        await _repository.InsertAsync(albums);
    }

    /// <summary>
    /// 更新文章专辑
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("更新文章专辑")]
    [HttpPut("edit")]
    public async Task Update(UpdateAlbumInput dto)
    {
        var albums = await _repository.GetByIdAsync(dto.Id);
        if (albums == null) throw Oops.Oh("无效参数");
        dto.Adapt(albums);
        await _repository.UpdateAsync(albums);
    }

    /// <summary>
    /// 文章专辑下拉选项
    /// </summary>
    /// <returns></returns>
    [DisplayName("文章专辑下拉选项")]
    [HttpGet]
    public async Task<List<SelectOutput>> Select()
    {
        return await _repository.AsQueryable().Where(x => x.Status == AvailabilityStatus.Enable)
            .OrderBy(x => x.Sort)
            .Select(x => new SelectOutput()
            {
                Value = x.Id,
                Label = x.Name
            })
            .ToListAsync();
    }
}