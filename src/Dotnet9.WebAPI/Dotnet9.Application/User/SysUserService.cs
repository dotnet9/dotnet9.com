using Dotnet9.Application.Auth;
using Dotnet9.Application.Config;
using Dotnet9.Application.Menu;
using Dotnet9.Application.User.Dtos;

namespace Dotnet9.Application.User;

/// <summary>
/// 系统用户管理
/// </summary>
public class SysUserService : BaseService<SysUser>, ITransient
{
    private readonly ISqlSugarRepository<SysUser> _repository;
    private readonly ISqlSugarRepository<SysUserRole> _userRoleRepository;
    private readonly ISqlSugarRepository<SysOrganization> _orgRepository;
    private readonly SysMenuService _sysMenuService;
    private readonly CustomConfigService _customConfigService;
    private readonly AuthManager _authManager;
    private readonly IEasyCachingProvider _easyCachingProvider;
    private readonly IIdGenerator _idGenerator;

    public SysUserService(ISqlSugarRepository<SysUser> repository,
        ISqlSugarRepository<SysUserRole> userRoleRepository,
        ISqlSugarRepository<SysOrganization> orgRepository,
        SysMenuService sysMenuService,
        CustomConfigService customConfigService,
        AuthManager authManager,
        IEasyCachingProvider easyCachingProvider,
        IIdGenerator idGenerator) : base(repository)
    {
        _repository = repository;
        _userRoleRepository = userRoleRepository;
        _orgRepository = orgRepository;
        _sysMenuService = sysMenuService;
        _customConfigService = customConfigService;
        _authManager = authManager;
        _easyCachingProvider = easyCachingProvider;
        _idGenerator = idGenerator;
    }

    /// <summary>
    /// 系统用户分页查询
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("系统用户分页查询")]
    [HttpGet]
    public async Task<PageResult<SysUserPageOutput>> Page([FromQuery] QuerySysUserInput dto)
    {
        List<long> orgIdList = new List<long>();
        if (dto.OrgId.HasValue)
        {
            orgIdList.Add(dto.OrgId.Value);
            var list = await _orgRepository.AsQueryable().ToChildListAsync(x => x.Children, dto.OrgId);
            orgIdList.AddRange(list.Select(x => x.Id));
        }
        return await _repository.AsQueryable()
            .Where(x => x.Id > 1)
            .WhereIF(!string.IsNullOrWhiteSpace(dto.Name), x => x.Name.Contains(dto.Name))
            .WhereIF(!string.IsNullOrWhiteSpace(dto.Account), x => x.Account.Contains(dto.Account))
            .WhereIF(!string.IsNullOrWhiteSpace(dto.Mobile), x => x.Mobile.Contains(dto.Mobile))
            .WhereIF(orgIdList.Any(), x => orgIdList.Contains(x.OrgId))
            .Select(x => new SysUserPageOutput
            {
                Name = x.Name,
                Status = x.Status,
                Account = x.Account,
                Birthday = x.Birthday,
                Mobile = x.Mobile,
                Gender = x.Gender,
                NickName = x.NickName,
                CreatedTime = x.CreatedTime,
                Email = x.Email,
                Id = x.Id
            }).ToPagedListAsync(dto);
    }

    /// <summary>
    /// 添加系统用户
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [UnitOfWork, HttpPost("add")]
    [DisplayName("添加系统用户")]
    public async Task AddUser(AddSysUserInput dto)
    {
        var user = dto.Adapt<SysUser>();
        user.Id = _idGenerator.NextId();
        string encode = _idGenerator.Encode(user.Id);
        var setting = await _customConfigService.Get<SysSecuritySetting>();
        user.Password = MD5Encryption.Encrypt(encode + (setting?.Password ?? "123456"));
        var roles = dto.Roles.Select(x => new SysUserRole()
        {
            RoleId = x,
            UserId = user.Id
        }).ToList();
        await _repository.InsertAsync(user);
        await _userRoleRepository.InsertRangeAsync(roles);
    }

    /// <summary>
    /// 更新系统用户信息
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("更新系统用户信息")]
    [UnitOfWork, HttpPut("edit")]
    public async Task UpdateUser(UpdateSysUserInput dto)
    {
        var user = await _repository.GetByIdAsync(dto.Id);
        if (user == null) throw Oops.Bah("无效参数");

        dto.Adapt(user);
        var roles = dto.Roles.Select(x => new SysUserRole()
        {
            RoleId = x,
            UserId = user.Id
        }).ToList();
        await _repository.UpdateAsync(user);
        await _userRoleRepository.DeleteAsync(x => x.UserId == user.Id);
        await _userRoleRepository.InsertRangeAsync(roles);
        await _easyCachingProvider.RemoveByPrefixAsync(CacheConst.PermissionKey);
    }

    /// <summary>
    /// 系统用户详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<UpdateSysUserInput> Detail([FromQuery] long id)
    {
        return await _repository.AsQueryable().Where(x => x.Id == id)
              .Select(x => new UpdateSysUserInput()
              {
                  Id = x.Id,
                  Name = x.Name,
                  Status = x.Status,
                  OrgId = x.OrgId,
                  Account = x.Account,
                  Mobile = x.Mobile,
                  Remark = x.Remark,
                  Birthday = x.Birthday,
                  Email = x.Email,
                  Gender = x.Gender,
                  NickName = x.NickName,
                  Roles = SqlFunc.Subqueryable<SysUserRole>().Where(s => s.UserId == x.Id).ToList(s => s.RoleId)
              }).FirstAsync();
    }

    /// <summary>
    /// 重置系统用户密码
    /// </summary>
    /// <returns></returns>
    [DisplayName("重置系统用户密码")]
    [HttpPatch]
    public async Task Reset(ResetPasswordInput dto)
    {
        string encrypt = MD5Encryption.Encrypt(_idGenerator.Encode(dto.Id) + dto.Password);
        await _repository.UpdateAsync(x => new SysUser()
        {
            Password = encrypt
        }, x => x.Id == dto.Id);
    }

    /// <summary>
    /// 获取当前登录用户的信息
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取登录用户的信息")]
    [HttpGet]
    public async Task<SysUserInfoOutput> CurrentUserInfo()
    {
        var userId = _authManager.UserId;
        return await _repository.AsQueryable().Where(x => x.Id == userId)
              .Select(x => new SysUserInfoOutput
              {
                  Name = x.Name,
                  Account = x.Account,
                  Avatar = x.Avatar,
                  Birthday = x.Birthday,
                  Email = x.Email,
                  Gender = x.Gender,
                  NickName = x.NickName,
                  Remark = x.Remark,
                  LastLoginIp = x.LastLoginIp,
                  LastLoginAddress = x.LastLoginAddress,
                  Mobile = x.Mobile,
                  OrgId = x.OrgId,
                  OrgName = SqlFunc.Subqueryable<SysOrganization>().Where(o => o.Id == x.OrgId).Select(o => o.Name)
              })
              .Mapper(dto =>
              {
                  if (_authManager.IsSuperAdmin)
                  {
                      dto.AuthBtnList = _repository.AsSugarClient().Queryable<SysMenu>().Where(x => x.Type == MenuType.Button)
                            .Select(x => x.Code).ToList();
                  }
                  else
                  {
                      var list = _sysMenuService.GetAuthButtonCodeList(userId).GetAwaiter().GetResult();
                      dto.AuthBtnList = list.Where(x => x.Access).Select(x => x.Code).ToList();
                  }
              })
              .FirstAsync();
    }

    /// <summary>
    /// 用户修改账户密码
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("用户修改账户密码")]
    [HttpPatch]
    public async Task ChangePassword(ChangePasswordOutput dto)
    {
        var userId = _authManager.UserId;
        string encode = _idGenerator.Encode(userId);
        string pwd = MD5Encryption.Encrypt($"{encode}{dto.OriginalPwd}");
        if (!await _repository.IsAnyAsync(x => x.Id == userId && x.Password == pwd))
        {
            throw Oops.Bah("原密码错误");
        }
        pwd = MD5Encryption.Encrypt($"{encode}{dto.Password}");
        await _repository.AsSugarClient().Updateable<SysUser>()
            .SetColumns(x => new SysUser()
            {
                Password = pwd
            })
            .Where(x => x.Id == userId)
            .ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 用户修改头像
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    [DisplayName("用户修改头像")]
    [HttpPatch]
    public async Task UploadAvatar([FromBody]string url)
    {
        long userId = _authManager.UserId;
        await _repository.UpdateAsync(x => new SysUser()
        {
            Avatar = url
        }, x => x.Id == userId);
    }

    /// <summary>
    /// 系统用户修改自己的信息
    /// </summary>
    /// <returns></returns>
    [DisplayName("系统用户修改个人信息")]
    [HttpPatch("updateCurrentUser")]
    public async Task UpdateCurrentUser(UpdateCurrentUserInput dto)
    {
        long userId = _authManager.UserId;
        await _repository.UpdateAsync(x => new SysUser()
        {
            Name = dto.Name,
            Birthday = dto.Birthday,
            Email = dto.Email,
            Gender = dto.Gender,
            Mobile = dto.Mobile,
            NickName = dto.NickName
        }, x => x.Id == userId);
    }
}