namespace Dotnet9.WebApp.Global.Config;

public class GlobalConfig
{
    public GlobalConfig(CookieStorage cookieStorage, IHttpContextAccessor httpContextAccessor)
    {
        _cookieStorage = cookieStorage;
        if (httpContextAccessor.HttpContext is not null)
            Initialization(httpContextAccessor.HttpContext.Request.Cookies);
    }

    #region Method

    public void Initialization(IRequestCookieCollection cookies)
    {
        _isDark = Convert.ToBoolean(cookies[IsDarkCookieKey]);
        _pageMode = cookies[PageModeKey];
        _navigationMini = Convert.ToBoolean(cookies[NavigationMiniCookieKey]);
        _expandOnHover = Convert.ToBoolean(cookies[ExpandOnHoverCookieKey]);
        _favorite = cookies[FavoriteCookieKey];
    }

    #endregion

    #region Field

    private bool _isDark;
    private string? _pageMode;
    private bool _expandOnHover;
    private bool _navigationMini;
    private string? _favorite;
    private NavModel? _currentNav;
    private readonly CookieStorage? _cookieStorage;

    #endregion

    #region Property

    public static string IsDarkCookieKey { get; set; } = "GlobalConfig_IsDark";

    public static string PageModeKey { get; set; } = "GlobalConfig_PageMode";

    public static string NavigationMiniCookieKey { get; set; } = "GlobalConfig_NavigationMini";

    public static string ExpandOnHoverCookieKey { get; set; } = "GlobalConfig_ExpandOnHover";

    public static string FavoriteCookieKey { get; set; } = "GlobalConfig_Favorite";

    public bool IsDark
    {
        get => _isDark;
        set
        {
            _isDark = value;
            _cookieStorage?.SetItemAsync(IsDarkCookieKey, value);
        }
    }

    public string PageMode
    {
        get => _pageMode ?? PageModes.PageTab;
        set
        {
            _pageMode = value;
            _cookieStorage?.SetItemAsync(PageModeKey, value);
            OnPageModeChanged?.Invoke();
        }
    }

    public bool NavigationMini
    {
        get => _navigationMini;
        set
        {
            _navigationMini = value;
            _cookieStorage?.SetItemAsync(NavigationMiniCookieKey, value);
        }
    }

    public bool ExpandOnHover
    {
        get => _expandOnHover;
        set
        {
            _expandOnHover = value;
            _cookieStorage?.SetItemAsync(NavigationMiniCookieKey, value);
        }
    }

    public string? Favorite
    {
        get => _favorite;
        set
        {
            _favorite = value;
            _cookieStorage?.SetItemAsync(FavoriteCookieKey, value);
        }
    }

    public NavModel? CurrentNav
    {
        get => _currentNav;
        set
        {
            _currentNav = value;
            OnCurrentNavChanged?.Invoke();
        }
    }

    #endregion

    #region event

    public delegate void GlobalConfigChanged();

    public event GlobalConfigChanged? OnPageModeChanged;
    public event GlobalConfigChanged? OnCurrentNavChanged;

    #endregion
}