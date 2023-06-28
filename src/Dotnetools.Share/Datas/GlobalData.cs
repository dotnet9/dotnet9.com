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
                $"{ToolUrlPrefix}rgb"),
            new(ToolKind.ToolStringEncoder, "在线字符串编码工具",
                $"{ToolImagePrefix}/stringencode.png",
                $"{ToolUrlPrefix}string-encoder"),
            new(ToolKind.ToolCountDown, "倒计时",
                $"{ToolImagePrefix}/countdown.png", $"{ToolUrlPrefix}countdown"),
            new(ToolKind.ToolTimestamp, "时间戳", $"{ToolImagePrefix}/timestamp.png",
                $"{ToolUrlPrefix}timestamp")
        };

    public static List<ToolItem> GameItems =>
        _gameItems ??= new List<ToolItem>()
        {
            new(ToolKind.GameTetris, "俄罗斯方块",
                $"{GameImagePrefix}/tetris-bookmark.png", $"{GameUrlPrefix}tetris"),
            new(ToolKind.GameMinesweeper, "扫雷游戏",
                $"{GameImagePrefix}/minesweeper.png", $"{GameUrlPrefix}minesweeper"),
            new(ToolKind.GameGuessingNumbers, "猜数字游戏",
                $"{GameImagePrefix}/guessing-numbers.png", $"{GameUrlPrefix}guessing-numbers"),
            new(ToolKind.GameTictactoe, "井字棋游戏",
                $"{GameImagePrefix}/tictactoe.png", $"{GameUrlPrefix}tictactoe"),
        };

    public static ToolItem? ToolItem(this ToolKind kind)
    {
        return ToolItems.FirstOrDefault(toolItem => toolItem.Kind == kind);
    }

    public static ToolItem? GameItem(this ToolKind kind)
    {
        return GameItems.FirstOrDefault(gameItem => gameItem.Kind == kind);
    }
}