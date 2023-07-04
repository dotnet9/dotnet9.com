using Dotnetools.Share.Models;

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
            new(ToolKind.ToolRegexTester, "正则表达式在线验证工具", $"{ToolImagePrefix}/regextester.png",
                $"{ToolUrlPrefix}regextester"),
            new(ToolKind.ToolJsonFormatter, "JSON格式化",
                $"{ToolImagePrefix}/jsonformatter.png",
                $"{ToolUrlPrefix}jsonformatter"),
            new(ToolKind.ToolRgb, "颜色值转换",
                $"{ToolImagePrefix}/rgb.png",
                $"{ToolUrlPrefix}rgb", 
                LearnUrl:"https://sunpma.com/other/rgb/",
                Github:"/Tools/RGB.razor"),
            new(ToolKind.ToolStringEncoder, "在线字符串编码工具",
                $"{ToolImagePrefix}/stringencode.png",
                $"{ToolUrlPrefix}string-encoder"),
            new(ToolKind.ToolCountDown, "倒计时",
                $"{ToolImagePrefix}/countdown.png", $"{ToolUrlPrefix}countdown"),
            new(ToolKind.ToolTimestamp, "时间戳", $"{ToolImagePrefix}/timestamp.png",
                $"{ToolUrlPrefix}timestamp"),
            new(ToolKind.ToolTimestamp, "Js代码转C#代码", $"{ToolImagePrefix}/timestamp.png",
                $"{ToolUrlPrefix}js2csharp")
        };

    public static List<ToolItem> GameItems =>
        _gameItems ??= new List<ToolItem>()
        {
            new(ToolKind.GameTetris, "俄罗斯方块",
                $"{GameImagePrefix}/tetris-bookmark.png", $"{GameUrlPrefix}tetris"),
            new(ToolKind.GameMinesweeper, "扫雷游戏",
                $"{GameImagePrefix}/minesweeper.png", $"{GameUrlPrefix}minesweeper", LearnUrl:"https://github.com/jarDotNet/BlazorMinesweeper"),
            new(ToolKind.GameGuessingNumbers, "猜数字游戏",
                $"{GameImagePrefix}/guessing-numbers.png", $"{GameUrlPrefix}guessing-numbers"),
            new(ToolKind.GameTictactoe, "井字棋游戏",
                $"{GameImagePrefix}/tictactoe.png", $"{GameUrlPrefix}tictactoe"),
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