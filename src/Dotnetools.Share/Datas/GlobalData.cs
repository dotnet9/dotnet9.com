namespace Dotnetools.Share.Datas;

public static class GlobalData
{
    private static List<ToolItem>? _toolItems;
    private const string ToolImagePrefix = "https://img1.dotnet9.com/tools/";
    private const string ToolUrlPrefix = "/tools/";

    private static List<ToolItem>? _gameItems;
    private const string GameImagePrefix = "https://img1.dotnet9.com/games/";
    private const string GameUrlPrefix = "/games/";

    public static List<ToolItem> ToolItems =>
        _toolItems ??= new List<ToolItem>()
        {
            new(ToolKind.ToolRegexTester,
                "正则表达式在线验证工具",
                $"{ToolImagePrefix}/regextester.png",
                $"{ToolUrlPrefix}regextester",
                Github: $"/Tools/{nameof(RegexTester)}.razor"),

            new(ToolKind.ToolJsonFormatter,
                "JSON格式化",
                $"{ToolImagePrefix}/jsonformatter.png",
                $"{ToolUrlPrefix}jsonformatter",
                Github: $"/Tools/{nameof(JsonFormatter)}.razor"),

            new(ToolKind.ToolRgb,
                "颜色值转换",
                $"{ToolImagePrefix}/rgb.png",
                $"{ToolUrlPrefix}rgb",
                LearnUrl: "https://sunpma.com/other/rgb/",
                Github: $"/Tools/{nameof(RGB)}.razor"),

            new(ToolKind.ToolStringEncoder, "在线字符串编码工具",
                $"{ToolImagePrefix}/stringencode.png",
                $"{ToolUrlPrefix}string-encoder",
                Github: $"/Tools/{nameof(StringEncoder)}.razor"),

            new(ToolKind.ToolCountDown, "倒计时",
                $"{ToolImagePrefix}/countdown.png",
                $"{ToolUrlPrefix}countdown",
                Github: $"/Tools/{nameof(CountDown)}.razor"),

            new(ToolKind.ToolTimestamp,
                "时间戳",
                $"{ToolImagePrefix}/timestamp.png",
                $"{ToolUrlPrefix}timestamp",
                Github: $"/Tools/{nameof(Timestamp)}.razor")
        };

    public static List<ToolItem> GameItems =>
        _gameItems ??= new List<ToolItem>()
        {
            new(ToolKind.GameTetris,
                "俄罗斯方块",
                $"{GameImagePrefix}/tetris-bookmark.png",
                $"{GameUrlPrefix}tetris",
                Github: $"/Games/{nameof(Tetris)}.razor"),

            new(ToolKind.GameMinesweeper,
                "经典扫雷",
                $"{GameImagePrefix}/minesweeper.png",
                $"{GameUrlPrefix}minesweeper",
                Github: $"/Games/{nameof(Minesweeper)}.razor",
                LearnUrl: "https://github.com/jarDotNet/BlazorMinesweeper"),

            new(ToolKind.GameGuessingNumbers,
                "猜数字游戏",
                $"{GameImagePrefix}/guessing-numbers.png",
                $"{GameUrlPrefix}guessing-numbers",
                Github: $"/Games/{nameof(GuessingNumbers)}.razor"),

            new(ToolKind.GameTictactoe,
                "井字棋游戏",
                $"{GameImagePrefix}/tictactoe.png",
                $"{GameUrlPrefix}tictactoe",
                Github: $"/Games/{nameof(Tictactoe)}.razor"),
        };

    private static List<ToolItem>? _allItems;

    private static List<ToolItem> AllItems
    {
        get
        {
            if (_allItems == null)
            {
                _allItems = new();
                _allItems.AddRange(ToolItems);
                _allItems.AddRange(GameItems);
            }

            return _allItems;
        }
    }

    public static ToolItem? Item(this ToolKind kind)
    {
        return AllItems.FirstOrDefault(toolItem => toolItem.Kind == kind);
    }

    public static ToolItem? Item(this string urlEnd)
    {
        return AllItems.FirstOrDefault(toolItem => toolItem.Url.EndsWith(urlEnd));
    }
}