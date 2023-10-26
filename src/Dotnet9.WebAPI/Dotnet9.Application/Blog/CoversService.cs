using Dotnet9.Application.Blog.Dtos;

namespace Dotnet9.Application.Blog;
/// <summary>
/// 模块封面管理
/// </summary>
public class CoversService : BaseService<Covers>
{
    private readonly ISqlSugarRepository<Covers> _repository;

    public CoversService(ISqlSugarRepository<Covers> repository) : base(repository)
    {
        _repository = repository;
    }

    [DisplayName("模块封面列表分页查询")]
    [HttpGet]
    public async Task<PageResult<CoversPageOutput>> Page([FromQuery] CoversPageQueryInput dto)
    {
        return await _repository.AsQueryable()
            .WhereIF(!string.IsNullOrWhiteSpace(dto.Name), x => x.Name.Contains(dto.Name))
            .WhereIF(dto.Type.HasValue, x => x.Type == dto.Type)
            .OrderBy(x => x.Sort)
            .Select(x => new CoversPageOutput
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
    /// 新增模块封面
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("新增模块封面")]
    [HttpPost("add")]
    public async Task Add(AddCoversInput dto)
    {
        var covers = dto.Adapt<Covers>();
        await _repository.InsertAsync(covers);
    }

    /// <summary>
    /// 更新模块封面
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("更新模块封面信息")]
    [HttpPut("edit")]
    public async Task Update(UpdateCoversInput dto)
    {
        var covers = await _repository.GetByIdAsync(dto.Id);
        if (covers == null) throw Oops.Bah("无效参数");
        dto.Adapt(covers);
        await _repository.UpdateAsync(covers);
    }


}