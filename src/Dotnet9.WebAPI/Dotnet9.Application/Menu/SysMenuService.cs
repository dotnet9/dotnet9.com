using Dotnet9.Application.Auth;
using Dotnet9.Application.Menu.Dtos;
using Microsoft.OpenApi.Extensions;

namespace Dotnet9.Application.Menu;
/// <summary>
/// 系统菜单管理
/// </summary>
public class SysMenuService : BaseService<SysMenu>, ITransient
{
    private readonly ISqlSugarRepository<SysMenu> _sysMenuRepository;
    private readonly IEasyCachingProvider _easyCachingProvider;
    private readonly AuthManager _authManager;

    public SysMenuService(ISqlSugarRepository<SysMenu> sysMenuRepository,
        IEasyCachingProvider easyCachingProvider,
        AuthManager authManager) : base(sysMenuRepository)
    {
        _sysMenuRepository = sysMenuRepository;
        _easyCachingProvider = easyCachingProvider;
        _authManager = authManager;
    }

    /// <summary>
    /// 菜单列表查询
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    [DisplayName("菜单列表查询")]
    [HttpGet]
    public async Task<List<SysMenuPageOutput>> Page([FromQuery] string name)
    {
        if (_authManager.IsSuperAdmin)
        {
            var q1 = _sysMenuRepository.AsQueryable()
                .OrderBy(x => x.Sort)
                .OrderBy(x => x.Id);
            if (!string.IsNullOrWhiteSpace(name))
            {
                var list = await q1.Where(x => x.Name.Contains(name)).ToListAsync();
                return list.Adapt<List<SysMenuPageOutput>>();
            }

            var menus = await q1.ToTreeAsync(x => x.Children, x => x.ParentId, null);
            return menus.Adapt<List<SysMenuPageOutput>>();
        }
        else
        {
            long userId = _authManager.UserId;
            var q2 = _sysMenuRepository.AsQueryable().InnerJoin<SysRoleMenu>((menu, roleMenu) => menu.Id == roleMenu.MenuId)
                .InnerJoin<SysRole>((menu, roleMenu, role) => roleMenu.RoleId == role.Id)
                .InnerJoin<SysUserRole>((menu, roleMenu, role, userRole) => role.Id == userRole.RoleId)
                .Where((menu, roleMenu, role, userRole) => role.Status == AvailabilityStatus.Enable && userRole.UserId == userId);
            if (!string.IsNullOrWhiteSpace(name))
            {
                var list = await q2.Where(menu => menu.Name.Contains(name)).Distinct().OrderBy(menu => menu.Sort).OrderBy(menu => menu.Id).ToListAsync();
                return list.Adapt<List<SysMenuPageOutput>>();
            }

            var menuIdList = await q2.Select(menu => menu.Id).ToListAsync();
            var array = menuIdList.Select(x => x as object).ToArray();
            var menus = await _sysMenuRepository.AsQueryable()
                .Where(x => x.Status == AvailabilityStatus.Enable)
                .OrderBy(x => x.Sort)
                .OrderBy(x => x.Id)
                .ToTreeAsync(x => x.Children, x => x.ParentId, null, array);
            return menus.Adapt<List<SysMenuPageOutput>>();
        }
    }

    /// <summary>
    /// 添加菜单/按钮
    /// </summary>
    /// <returns></returns>
    [DisplayName("添加菜单/按钮")]
    [HttpPost("add")]
    public async Task AddMenu(AddSysMenuInput dto)
    {
        SysMenu sysMenu = dto.Adapt<SysMenu>();
        if (sysMenu.Type == MenuType.Button)
        {
            sysMenu.Link = sysMenu.Icon = sysMenu.Component = sysMenu.Path = sysMenu.Redirect = sysMenu.RouteName = null;
        }
        else
        {
            if (await _sysMenuRepository.IsAnyAsync(x => x.RouteName.ToLower() == dto.RouteName.ToLower()))
            {
                throw Oops.Bah("路由名称已存在");
            }
            sysMenu.Code = null;
        }
        await _sysMenuRepository.InsertAsync(sysMenu);
        await _easyCachingProvider.RemoveByPrefixAsync(CacheConst.PermissionKey);
    }

    /// <summary>
    /// 修改菜单/按钮
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("更新菜单/按钮")]
    [HttpPut("edit")]
    public async Task UpdateMenu(UpdateSysMenuInput dto)
    {
        SysMenu sysMenu = await _sysMenuRepository.GetByIdAsync(dto.Id);
        if (sysMenu == null)
        {
            throw Oops.Bah("无线参数");
        }

        if (dto.Type != MenuType.Button && await _sysMenuRepository.IsAnyAsync(x => x.RouteName.ToLower() == dto.RouteName.ToLower() && x.Id != dto.Id))
        {
            throw Oops.Bah("路由名称已存在");
        }
        //检查菜单父子关系是否存在循环引用
        if (dto.ParentId.HasValue && dto.ParentId != sysMenu.ParentId)
        {
            List<SysMenu> list = await _sysMenuRepository.AsQueryable().ToChildListAsync(x => x.Children, dto.Id);
            if (list.Any(x => x.Id == dto.ParentId))
            {
                throw Oops.Bah($"请勿将当前{dto.Type.GetDisplayName()}的父级菜单设置为它的子级");
            }
        }

        dto.Adapt(sysMenu);
        if (sysMenu.Type == MenuType.Button)
        {
            sysMenu.Link = sysMenu.Icon = sysMenu.Component = sysMenu.Path = sysMenu.Redirect = sysMenu.RouteName = null;
        }
        else
        {
            sysMenu.Code = null;
        }

        await _sysMenuRepository.UpdateAsync(sysMenu);
        await _easyCachingProvider.RemoveByPrefixAsync(CacheConst.PermissionKey);
    }

    /// <summary>
    /// 根据菜单Id获取系统菜单详情
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [DisplayName("系统菜单详情")]
    [HttpGet]
    public async Task<SysMenuDetailOutput> Detail([FromQuery] long id)
    {
        return await _sysMenuRepository.AsQueryable().Where(x => x.Id == id)
            .Select(x => new SysMenuDetailOutput
            {
                Id = x.Id,
                Name = x.Name,
                ParentId = x.ParentId,
                Status = x.Status,
                Code = x.Code,
                Sort = x.Sort,
                Component = x.Component,
                Icon = x.Icon,
                IsFixed = x.IsFixed,
                IsIframe = x.IsIframe,
                IsKeepAlive = x.IsKeepAlive,
                IsVisible = x.IsVisible,
                Link = x.Link,
                Remark = x.Remark,
                Path = x.Path,
                Redirect = x.Redirect,
                RouteName = x.RouteName,
                Type = x.Type
            }).FirstAsync();
    }

    /// <summary>
    /// 菜单下拉树
    /// </summary>
    /// <returns></returns>
    [DisplayName("菜单下拉树")]
    [HttpGet]
    public async Task<List<TreeSelectOutput>> TreeSelect()
    {
        var list = await _sysMenuRepository.AsQueryable()
            .OrderBy(x => x.Sort)
            .OrderBy(x => x.Id)
            .WithCache()
            .ToTreeAsync(x => x.Children, x => x.ParentId, null);
        return list.Adapt<List<TreeSelectOutput>>();
    }

    /// <summary>
    /// 删除菜单/按钮
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("删除菜单/按钮"), HttpDelete("delete")]
    public override async Task Delete(KeyDto dto)
    {
        await base.Delete(dto);
        await _easyCachingProvider.RemoveByPrefixAsync(CacheConst.PermissionKey);
    }

    /// <summary>
    /// 修改菜单/按钮状态
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [DisplayName("修改菜单/按钮状态"), HttpPut("setStatus")]
    public override async Task SetStatus(AvailabilityDto dto)
    {
        await base.SetStatus(dto);
        await _easyCachingProvider.RemoveByPrefixAsync(CacheConst.PermissionKey);
    }

    /// <summary>
    /// 获取当前登录用户可用菜单
    /// </summary>
    /// <returns></returns>
    [DisplayName("获取当前登录用户可用菜单")]
    [HttpGet]
    public async Task<List<RouterOutput>> PermissionMenus()
    {
        long userId = _authManager.UserId;
        var value = await _easyCachingProvider.GetAsync($"{CacheConst.PermissionMenuKey}{userId}", async () =>
        {
            var queryable = _sysMenuRepository.AsQueryable()
                .Where(x => x.Status == AvailabilityStatus.Enable)
                .OrderBy(x => x.Sort)
                .OrderBy(x => x.Id);
            List<SysMenu> list;
            if (_authManager.IsSuperAdmin)
            {
                list = await queryable
                    .Where(x => x.Type != MenuType.Button)
                    .ToTreeAsync(x => x.Children, x => x.ParentId, null);
            }
            else
            {
                List<long> menuIdList = await _sysMenuRepository.AsSugarClient().Queryable<SysRole>().InnerJoin<SysUserRole>((role, userRole) => userRole.RoleId == role.Id)
                    .InnerJoin<SysRoleMenu>((role, userRole, roleMenu) => role.Id == roleMenu.RoleId)
                    .InnerJoin<SysMenu>((role, userRole, roleMenu, menu) => roleMenu.MenuId == menu.Id)
                    .Where((role, userRole, roleMenu, menu) => role.Status == AvailabilityStatus.Enable && userRole.UserId == userId && menu.Status == AvailabilityStatus.Enable)
                    .Select((role, userRole, roleMenu) => roleMenu.MenuId).ToListAsync();
                var array = menuIdList.Select(x => x as object).ToArray();
                list = await queryable
                .ToTreeAsync(x => x.Children, x => x.ParentId, null, array);
                RemoveButton(list);
            }
            return list.Adapt<List<RouterOutput>>();
        }, TimeSpan.FromDays(1));
        return value.Value ?? new List<RouterOutput>();
    }

    /// <summary>
    /// 菜单按钮树
    /// </summary>
    /// <returns></returns>
    [DisplayName("菜单按钮树")]
    [HttpGet]
    public async Task<List<TreeSelectOutput>> TreeMenuButton()
    {
        long userId = _authManager.UserId;
        List<SysMenu> menus;
        if (_authManager.IsSuperAdmin)//超级管理员
        {
            menus = await _sysMenuRepository.AsQueryable().ToTreeAsync(x => x.Children, x => x.ParentId, null);
        }
        else
        {
            menus = await _sysMenuRepository.AsQueryable()
                .InnerJoin<SysRoleMenu>((menu, roleMenu) => menu.Id == roleMenu.MenuId)
                .InnerJoin<SysRole>((menu, roleMenu, role) => roleMenu.RoleId == role.Id)
                .InnerJoin<SysUserRole>((menu, roleMenu, role, userRole) => role.Id == userRole.RoleId)
                .Where((menu, roleMenu, role, userRole) => menu.Status == AvailabilityStatus.Enable &&
                                                           role.Status == AvailabilityStatus.Enable &&
                                                           userRole.UserId == userId)
                .Select(menu => menu)
                .ToTreeAsync(x => x.Children, x => x.ParentId, null);
        }

        return menus.Adapt<List<TreeSelectOutput>>();
    }

    /// <summary>
    /// 校验权限
    /// </summary>
    /// <param name="code">权限标识</param>
    /// <returns></returns>
    [NonAction]
    public async Task<bool> CheckPermission(string code)
    {
        if (_authManager.IsSuperAdmin) return true;
        var cache = await GetAuthButtonCodeList(_authManager.UserId);
        var output = cache.FirstOrDefault(x => x.Code.Contains(code, StringComparison.CurrentCultureIgnoreCase));
        return output?.Access ?? true;
    }

    /// <summary>
    /// 获取指定用户的访问权限集合
    /// </summary>
    /// <param name="userId">系统用户id</param>
    /// <returns></returns>
    [NonAction]
    public async Task<List<CheckPermissionOutput>> GetAuthButtonCodeList(long userId)
    {
        var cache = await _easyCachingProvider.GetAsync($"{CacheConst.PermissionButtonCodeKey}{userId}", async () =>
        {
            var queryable = _sysMenuRepository.AsSugarClient().Queryable<SysRole>()
                .InnerJoin<SysUserRole>((role, userRole) => role.Id == userRole.RoleId)
                .InnerJoin<SysRoleMenu>((role, userRole, roleMenu) => role.Id == roleMenu.RoleId)
                .Where(role => role.Status == AvailabilityStatus.Enable)
                .Select((role, userRole, roleMenu) => roleMenu);
            var list = await _sysMenuRepository.AsQueryable().LeftJoin(queryable, (menu, roleMenu) => menu.Id == roleMenu.MenuId)
                   .Where(menu => menu.Type == MenuType.Button)
                   .Select((menu, roleMenu) => new CheckPermissionOutput
                   {
                       Code = menu.Code,
                       Access = SqlFunc.IIF(SqlFunc.IsNull(roleMenu.Id, 0) > 0 || menu.Status == AvailabilityStatus.Disable, true, false)
                   }).ToListAsync();
            return list.Distinct().ToList();
        }, TimeSpan.FromDays(1));
        return cache.Value;
    }

    /// <summary>
    /// 移除菜单中的按钮
    /// </summary>
    /// <param name="menus"></param>
    void RemoveButton(List<SysMenu> menus)
    {
        for (int i = menus.Count - 1; i >= 0; i--)
        {
            if (menus[i].Type == MenuType.Button)
            {
                menus.Remove(menus[i]);
                continue;
            }
            if (menus[i].Children.Any())
            {
                RemoveButton(menus[i].Children);
            }
        }
    }
}