using Dotnet9.Application.Organization.Dtos;

namespace Dotnet9.Application.Organization;

/// <summary>
/// 组织机构管理
/// </summary>
public class SysOrganizationService : BaseService<SysOrganization>
{
    private readonly ISqlSugarRepository<SysOrganization> _orgSqlSugarRepository;

    public SysOrganizationService(ISqlSugarRepository<SysOrganization> orgSqlSugarRepository) : base(orgSqlSugarRepository)
    {
        _orgSqlSugarRepository = orgSqlSugarRepository;
    }

    /// <summary>
    /// 组织机构列表查询
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [Description("组织机构列表查询")]
    [HttpGet]
    public async Task<List<SysOrgPageOutput>> Page([FromQuery] string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            var list = await _orgSqlSugarRepository.AsQueryable().Where(x => x.Name.Contains(name)).ToListAsync();
            return list.Adapt<List<SysOrgPageOutput>>();
        }

        var tree = await _orgSqlSugarRepository.AsQueryable().OrderBy(x => x.Sort)
            .OrderBy(x => x.Id)
            .WithCache()
            .ToTreeAsync(x => x.Children, x => x.ParentId, null);
        return tree.Adapt<List<SysOrgPageOutput>>();
    }

    /// <summary>
    /// 添加组织机构
    /// </summary>
    /// <returns></returns>
    [Description("添加组织机构")]
    [HttpPost("add")]
    public async Task AddOrg(AddOrgInput dto)
    {
        var organization = dto.Adapt<SysOrganization>();
        await _orgSqlSugarRepository.InsertAsync(organization);
    }

    /// <summary>
    /// 更新组织机构
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [Description("更新组织机构")]
    [HttpPut("edit")]
    public async Task UpdateOrg(UpdateOrgInput dto)
    {
        var organization = await _orgSqlSugarRepository.GetByIdAsync(dto.Id);
        if (organization == null)
        {
            throw Oops.Bah("无效参数");
        }

        dto.Adapt(organization);
        await _orgSqlSugarRepository.UpdateAsync(organization);
    }

    /// <summary>
    /// 获取机构下拉选项
    /// </summary>
    /// <returns></returns>
    [Description("获取机构下拉选项")]
    [HttpGet]
    public async Task<List<TreeSelectOutput>> TreeSelect()
    {
        var list = await _orgSqlSugarRepository.AsQueryable().WithCache().ToTreeAsync(x => x.Children, x => x.ParentId, null);
        return list.Adapt<List<TreeSelectOutput>>();
    }
}