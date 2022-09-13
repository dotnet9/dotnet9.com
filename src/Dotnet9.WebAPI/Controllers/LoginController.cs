namespace Dotnet9.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public partial class LoginController : ControllerBase
{
    private readonly AboutManager _aboutManager;
    private readonly IAboutRepository _aboutRepository;
    private readonly AlbumManager _albumManager;
    private readonly IAlbumRepository _albumRepository;
    private readonly BlogPostManager _blogPostManager;
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly CategoryManager _categoryManager;
    private readonly ICategoryRepository _categoryRepository;
    private readonly Dotnet9DbContext _dbContext;
    private readonly DonationManager _donationManager;
    private readonly IDonationRepository _donationRepository;
    private readonly LinkManager _linkManager;
    private readonly ILinkRepository _linkRepository;
    private readonly IdManager _manager;
    private readonly PrivacyManager _privacyManager;
    private readonly IPrivacyRepository _privacyRepository;
    private readonly IIdRepository _repository;
    private readonly IOptionsSnapshot<SiteOptions> _siteOptions;
    private readonly TagManager _tagManager;
    private readonly ITagRepository _tagRepository;
    private readonly TimelineManager _timelineManager;
    private readonly ITimelineRepository _timelineRepository;

    public LoginController(IOptionsSnapshot<SiteOptions> siteOptions, Dotnet9DbContext dbContext, IdManager manager,
        IIdRepository repository, AboutManager aboutManager, IAboutRepository aboutRepository,
        AlbumManager albumManager, IAlbumRepository albumRepository, CategoryManager categoryManager,
        ICategoryRepository categoryRepository, TagManager tagManager, ITagRepository tagRepository,
        DonationManager donationManager, IDonationRepository donationRepository, LinkManager linkManager,
        ILinkRepository linkRepository, PrivacyManager privacyManager, IPrivacyRepository privacyRepository,
        TimelineManager timelineManager, ITimelineRepository timelineRepository, BlogPostManager blogPostManager,
        IBlogPostRepository blogPostRepository)
    {
        _siteOptions = siteOptions;
        _dbContext = dbContext;
        _manager = manager;
        _repository = repository;
        _aboutManager = aboutManager;
        _aboutRepository = aboutRepository;
        _albumManager = albumManager;
        _albumRepository = albumRepository;
        _categoryManager = categoryManager;
        _categoryRepository = categoryRepository;
        _tagManager = tagManager;
        _tagRepository = tagRepository;
        _donationManager = donationManager;
        _donationRepository = donationRepository;
        _linkManager = linkManager;
        _linkRepository = linkRepository;
        _privacyManager = privacyManager;
        _privacyRepository = privacyRepository;
        _timelineManager = timelineManager;
        _timelineRepository = timelineRepository;
        _blogPostManager = blogPostManager;
        _blogPostRepository = blogPostRepository;
        _siteOptions = siteOptions;
        _manager = manager;
        _repository = repository;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> CreateWorld()
    {
        if (await _repository.FindByNameAsync(UserRoleConst.Admin) != null)
        {
            return StatusCode((int)HttpStatusCode.Conflict, "已经初始化过了");
        }

        var user = new User(UserRoleConst.Admin);
        var r = await _repository.CreateAsync(user, "Dotnet9");
        Debug.Assert(r.Succeeded);
        var token = await _repository.GenerateChangedPhoneNumberTokenAsync(user, "18688888888");
        var cr = await _repository.ChangePhoneNumberAsync(user.Id, "18688888888", token);
        Debug.Assert(cr.Succeeded);
        r = await _repository.AddToRolesAsync(user, UserRoleConst.User);
        Debug.Assert(r.Succeeded);
        r = await _repository.AddToRolesAsync(user, UserRoleConst.Admin);
        Debug.Assert(r.Succeeded);
        return Ok();
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserResponse>> GetUserInfo()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _repository.FindByIdAsync(Guid.Parse(userId!));
        if (user == null)
        {
            return NotFound();
        }

        return new UserResponse(user.Id, user.PhoneNumber!, user.CreationTime);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<IdentityResult>> ChangePassword(ChangeMyPasswordRequest req)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _repository.FindByIdAsync(Guid.Parse(userId!));
        if (user == null)
        {
            return NotFound();
        }

        var checkResult = await _repository.CheckForSignInAsync(user, req.Password, false);
        if (!checkResult.Succeeded)
        {
            return checkResult.IsLockedOut
                ? StatusCode((int)HttpStatusCode.Locked, "用户已经被锁定")
                : BadRequest($"修改失败：{checkResult}");
        }

        var changeResult = await _repository.ChangePasswordAsync(user.Id, req.Password2);
        if (changeResult.Succeeded)
        {
            return changeResult;
        }

        return BadRequest("修改失败");
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<string?>> LoginByPhoneAndPwd(LoginByPhoneAndPwdRequest req)
    {
        var (checkResult, token) = await _manager.LoginByPhoneAndPwdAsync(req.PhoneNumber, req.Password);
        if (checkResult.Succeeded)
        {
            return token;
        }

        return checkResult.IsLockedOut
            ? StatusCode((int)HttpStatusCode.Locked, "用户已经被锁定")
            : BadRequest($"登录失败：{checkResult}");
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<string?>> LoginByUserNameAndPwd(
        LoginByUserNameAndPwdRequest req)
    {
        var (checkResult, token) = await _manager.LoginByUserNameAndPwdAsync(req.UserName, req.Password);
        if (checkResult.Succeeded)
        {
            return token;
        }

        return checkResult.IsLockedOut
            ? StatusCode((int)HttpStatusCode.Locked, "用户已经被锁定")
            : BadRequest($"登录失败：{checkResult}");
    }
}