using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Dotnet9.WebAPI.Services;

public partial class SeedService : ISeedService
{
    private readonly AboutManager _aboutManager;
    private readonly IAboutRepository _aboutRepository;
    private readonly AlbumManager _albumManager;
    private readonly BlogPostManager _blogPostManager;
    private readonly CategoryManager _categoryManager;
    private readonly Dotnet9DbContext _dbContext;
    private readonly DonationManager _donationManager;
    private readonly IDonationRepository _donationRepository;
    private readonly IIdRepository _iDRepository;
    private readonly LinkManager _linkManager;
    private readonly PrivacyManager _privacyManager;
    private readonly IPrivacyRepository _privacyRepository;
    private readonly SiteOptions _siteOptions;
    private readonly TagManager _tagManager;
    private readonly TimelineManager _timelineManager;

    public SeedService(Dotnet9DbContext dbContext, IOptions<SiteOptions> siteOptions, IIdRepository iDRepository,
        IAboutRepository aboutRepository, BlogPostManager blogPostManager, CategoryManager categoryManager,
        AlbumManager albumManager, TagManager tagManager, AboutManager aboutManager,
        IPrivacyRepository privacyRepository, PrivacyManager privacyManager, LinkManager linkManager,
        IDonationRepository donationRepository, DonationManager donationManager, TimelineManager timelineManager)
    {
        _dbContext = dbContext;
        _siteOptions = siteOptions.Value;
        _iDRepository = iDRepository;
        _aboutRepository = aboutRepository;
        _blogPostManager = blogPostManager;
        _categoryManager = categoryManager;
        _albumManager = albumManager;
        _tagManager = tagManager;
        _aboutManager = aboutManager;
        _privacyRepository = privacyRepository;
        _privacyManager = privacyManager;
        _linkManager = linkManager;
        _donationRepository = donationRepository;
        _donationManager = donationManager;
        _timelineManager = timelineManager;
    }

    public async Task MigrateAsync()
    {
        Console.WriteLine($"1、创建数据库表结构：数据库如果不存在，EnsureCreatedAsync会根据{nameof(Dotnet9DbContext)}配置创建");
        await CreateDatabaseAsync();
        Console.WriteLine("2、创建默认用户");
        await CreateDefaultUser();
        Console.WriteLine(
            $"3、添加种子数据，需要先将数据从 https://github.com/dotnet9/Assets.Dotnet9 克隆，并配置到 {_siteOptions.AssetsLocalPath}");
        await CreateSeedDataAsync();

        await Task.CompletedTask;
    }

    private async Task CreateDatabaseAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync();
    }

    private async Task CreateDefaultUser()
    {
        if (await _iDRepository.FindByNameAsync(UserRoleConst.Admin) != null)
        {
            Console.WriteLine("已经初始化过了");
            return;
        }

        User user = new User(UserRoleConst.Admin);
        IdentityResult r = await _iDRepository.CreateAsync(user, "Dotnet9");
        Debug.Assert(r.Succeeded);
        string token = await _iDRepository.GenerateChangedPhoneNumberTokenAsync(user, "18688888888");
        SignInResult cr = await _iDRepository.ChangePhoneNumberAsync(user.Id, "18688888888", token);
        Debug.Assert(cr.Succeeded);
        r = await _iDRepository.AddToRolesAsync(user, UserRoleConst.User);
        Debug.Assert(r.Succeeded);
        r = await _iDRepository.AddToRolesAsync(user, UserRoleConst.Admin);
        Debug.Assert(r.Succeeded);
    }
}