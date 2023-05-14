namespace Dotnet9.Service.Services;

public partial class SeedService : ISeedService
{
    private readonly AboutManager _aboutManager;
    private readonly IAboutRepository _aboutRepository;
    private readonly AlbumManager _albumManager;
    private readonly BlogManager _blogManager;
    private readonly CategoryManager _categoryManager;
    private readonly Dotnet9DbContext _dbContext;
    private readonly DonationManager _donationManager;
    private readonly IDonationRepository _donationRepository;
    private readonly FriendlyLinkManager _linkManager;
    private readonly PrivacyManager _privacyManager;
    private readonly IPrivacyRepository _privacyRepository;
    private readonly SiteOptions _siteOptions;
    private readonly TagManager _tagManager;
    private readonly TimelineManager _timelineManager;

    public SeedService(Dotnet9DbContext dbContext, IOptions<SiteOptions> siteOptions,
        IAboutRepository aboutRepository, BlogManager blogManager, CategoryManager categoryManager,
        AlbumManager albumManager, TagManager tagManager, AboutManager aboutManager,
        IPrivacyRepository privacyRepository, PrivacyManager privacyManager, FriendlyLinkManager linkManager,
        IDonationRepository donationRepository, DonationManager donationManager, TimelineManager timelineManager)
    {
        _dbContext = dbContext;
        _siteOptions = siteOptions.Value;
        _aboutRepository = aboutRepository;
        _blogManager = blogManager;
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
        Console.WriteLine($"创建数据库表结构：数据库如果不存在，EnsureCreatedAsync会根据{nameof(Dotnet9DbContext)}配置创建");
        await CreateDatabaseAsync();
        Console.WriteLine(
            $"3、添加种子数据，需要先将数据从 https://github.com/dotnet9/Assets.Dotnet9 克隆，并配置到 {_siteOptions.AssetsLocalPath}");
        await CreateSeedDataAsync();

        await Task.CompletedTask;
    }

    private async Task CreateDatabaseAsync()
    {
        await _dbContext.Database.EnsureCreatedAsync();
    }
}