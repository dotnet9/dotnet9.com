using Dotnet9.Application.Blog.Dtos;

namespace Dotnet9.Application.Blog;
/// <summary>
/// 文章栏目管理
/// </summary>
public class CategoryService : BaseService<Categories>
{
    private readonly ISqlSugarRepository<Categories> _repository;

    public CategoryService(ISqlSugarRepository<Categories> repository) : base(repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// 文章栏目列表
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [DisplayName("文章栏目列表")]
    [HttpGet]
    public async Task<List<CategoryPageOutput>> Page([FromQuery] string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            var list = await _repository.AsQueryable().Where(x => x.Name.Contains(name)).ToListAsync();
            return list.Adapt<List<CategoryPageOutput>>();
        }

        var tree = await _repository.AsQueryable().OrderBy(x => x.Sort)
            .OrderBy(x => x.Id)
            .WithCache()
            .ToTreeAsync(x => x.Children, x => x.ParentId, null);
        return tree.Adapt<List<CategoryPageOutput>>();
    }

    /// <summary>
    /// 添加文章栏目
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("添加文章栏目")]
    [HttpPost("add")]
    public async Task Add(AddCategoryInput dto)
    {
        var entity = dto.Adapt<Categories>();
        await _repository.InsertAsync(entity);
    }

    /// <summary>
    /// 更新文章栏目
    /// </summary>
    /// <returns></returns>
    [DisplayName("更新文章栏目")]
    [HttpPut("edit")]
    public async Task Update(UpdateCategoryInput dto)
    {
        var entity = await _repository.GetByIdAsync(dto.Id);
        if (entity == null) throw Oops.Bah("无效参数");
        dto.Adapt(entity);
        await _repository.UpdateAsync(entity);
    }

    /// <summary>
    /// 获取文章栏目下拉选项
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取文章栏目下拉选项")]
    [HttpGet]
    public async Task<List<TreeSelectOutput>> TreeSelect()
    {
        var list = await _repository.AsQueryable().WithCache().ToTreeAsync(x => x.Children, x => x.ParentId, null);
        return list.Adapt<List<TreeSelectOutput>>();
    }
}