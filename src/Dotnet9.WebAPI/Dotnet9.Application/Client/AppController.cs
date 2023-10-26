using Dotnet9.Application.Auth;
using Dotnet9.Application.Client.Dtos;
using Dotnet9.Application.Config;

namespace Dotnet9.Application.Client;
/// <summary>
/// 博客基本信息
/// </summary>
[AllowAnonymous]
[ApiDescriptionSettings("博客前端接口")]
public class AppController : IDynamicApiController
{
    private readonly CustomConfigService _customConfigService;
    private readonly ISqlSugarRepository<Covers> _coversRepository;
    private readonly AuthManager _authManager;
    private readonly ISqlSugarRepository<FriendLink> _linkRepository;

    public AppController(CustomConfigService customConfigService,
        ISqlSugarRepository<Covers> coversRepository,
        AuthManager authManager,
        ISqlSugarRepository<FriendLink> linkRepository)
    {
        _customConfigService = customConfigService;
        _coversRepository = coversRepository;
        _authManager = authManager;
        _linkRepository = linkRepository;
    }

    /// <summary>
    /// 博客基本信息
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<BlogOutput> Info()
    {
        var blogSetting = await _customConfigService.Get<BlogSetting>();

        var info = await _customConfigService.Get<BloggerInfo>();

        var pics = await _coversRepository.AsQueryable().InnerJoin<Pictures>((covers, pictures) => covers.Id == pictures.CoverId)
            .Where(covers => covers.Type.HasValue)
            .WithCache()
            .Select((covers, pictures) => new
            {
                covers.Type,
                pictures.Url
            }).ToListAsync();
        var dictionary = pics.GroupBy(x => x.Type)
            .ToDictionary(x => x.Key.ToString(), v => v.Select(x => x.Url).ToList());
        return new BlogOutput { Site = blogSetting, Info = info, Covers = dictionary };
    }

    /// <summary>
    /// 友情链接
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<FriendLinkOutput>> Links()
    {
        return await _linkRepository.AsQueryable().Where(x => x.Status == AvailabilityStatus.Enable)
              .OrderBy(x => x.Sort)
              .OrderBy(x => x.Id)
              .Select(x => new FriendLinkOutput
              {
                  Id = x.Id,
                  Link = x.Link,
                  Logo = x.Logo,
                  SiteName = x.SiteName,
                  Remark = x.Remark
              }).ToListAsync();
    }
}