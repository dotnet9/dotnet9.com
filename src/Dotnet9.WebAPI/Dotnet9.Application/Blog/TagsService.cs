using Dotnet9.Application.Blog.Dtos;

namespace Dotnet9.Application.Blog;

/// <summary>
/// 文章标签管理
/// </summary>
public class TagsService : BaseService<Tags>
{
    private readonly ISqlSugarRepository<Tags> _repository;

    public TagsService(ISqlSugarRepository<Tags> repository) : base(repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 文章标签列表分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("文章标签列表")]
    [HttpGet]
    public async Task<PageResult<TagsPageOutput>> Page([FromQuery] TagsPageQueryInput dto)
    {
        return await _repository.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(dto.Name), x => x.Name.Contains(dto.Name))
            .OrderBy(x => x.Sort)
            .OrderByDescending(x => x.Id)
            .Select(x => new TagsPageOutput
            {
                Id = x.Id,
                Name = x.Name,
                Status = x.Status,
                Sort = x.Sort,
                Cover = x.Cover,
                CreatedTime = x.CreatedTime,
                Color = x.Color
            }).ToPagedListAsync(dto);
    }

    /// <summary>
    /// 添加文章标签
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("添加文章标签")]
    [HttpPost("add")]
    public async Task Add(AddTagInput dto)
    {
        var tags = dto.Adapt<Tags>();
        await _repository.InsertAsync(tags);
    }

    /// <summary>
    /// 更新文章标签
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("更新文章标签")]
    [HttpPut("edit")]
    public async Task Update(UpdateTagInput dto)
    {
        var tags = await _repository.GetByIdAsync(dto.Id);
        if (tags == null) throw Oops.Oh("无效参数");
        dto.Adapt(tags);
        await _repository.UpdateAsync(tags);
    }

    /// <summary>
    /// 文章标签下拉选项
    /// </summary>
    /// <returns></returns>
    [DisplayName("文章标签下拉选项")]
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