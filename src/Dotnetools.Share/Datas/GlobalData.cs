using Dotnetools.Share.Models.Games;
using Dotnetools.Share.Models.Tools;

namespace Dotnetools.Share.Datas;

public static class GlobalData
{
    private static List<ToolItem>? _toolItems;
    private const string ToolImagePrefix = "https://img1.dotnet9.com/tools/";
    private const string ToolUrlPrefix = "/tools/";

    private static List<GameItem>? _gameItems;
    private const string GameImagePrefix = "https://img1.dotnet9.com/games/";
    private const string GameUrlPrefix = "/games/";

    public static List<ToolItem> ToolItems =>
        _toolItems ??= new List<ToolItem>()
        {
            new(ToolKind.RegexTester, "正则表达式在线验证工具",
                "这个示例演示了如何使用Blazor Server开发一个简单的正则表达式在线验证工具。用户可以输入正则表达式和测试字符串并单击“测试”按钮以测试正则表达式是否匹配测试字符串。此外，这个示例还提供了10几个常用的正则表达式测试，用户可以单击链接加载测试数据并自动填充正则表达式和测试字符串。",
                $"{ToolImagePrefix}/regextester.png", $"{ToolUrlPrefix}regextester"),
            new(ToolKind.JsonFormatter, "JSON格式化",
                "使用JsonDocument.Parse方法将输入的JSON字符串解析为JsonDocument对象。然后，我们使用JsonSerializer.Serialize方法将JsonDocument对象转换为格式化的JSON字符串。我们还使用JsonSerializerOptions来设置缩进选项，以便生成易于阅读的格式化JSON字符串。",
                $"{ToolImagePrefix}/jsonformatter.png",
                $"{ToolUrlPrefix}jsonformatter"),
            new(ToolKind.StringEncoder, "在线字符串编码工具",
                "使用System.Web.HttpUtility.UrlDecode方法对编码结果进行URL解码，System.Web.HttpUtility.UrlEncode方法对输入字符串进行URL编码",
                $"{ToolImagePrefix}/stringencode.png",
                $"{ToolUrlPrefix}string-encoder"),
            new(ToolKind.CountDown, "倒计时",
                "这个示例展示了如何在Blazor中使用异步方法和延迟来实现简单的倒计时功能。你可以根据需要进行扩展，例如添加暂停、重置等功能。",
                $"{ToolImagePrefix}/countdown.png", $"{ToolUrlPrefix}countdown"),
            new(ToolKind.Timestamp, "时间戳", "时间与时间戳相互转换", $"{ToolImagePrefix}/timestamp.png",
                $"{ToolUrlPrefix}timestamp")
        };

    public static List<GameItem> GameItems =>
        _gameItems ??= new List<GameItem>()
        {
            new(GameKind.Tetris, "俄罗斯方块",
                "不需要描述",
                $"{GameImagePrefix}/tetris-bookmark.png", $"{GameUrlPrefix}tetris"),
            new(GameKind.Minesweeper, "扫雷游戏",
                "在这个示例中，玩家需要点击方格来揭开它们。如果玩家踩到地雷，游戏结束。如果玩家揭开的方格周围有地雷，方格上会显示相应的数字，表示周围的地雷数量。如果玩家成功揭开所有没有地雷的方格，游戏胜利。",
                $"{GameImagePrefix}/minesweeper.png", $"{GameUrlPrefix}minesweeper"),
            new(GameKind.GuessingNumbers, "猜数字游戏",
                "游戏开始时，会生成一个1到100之间的随机数字作为目标数字。玩家需要通过输入数字来猜测目标数字，系统会根据玩家的猜测给出相应的提示。如果玩家猜对了，游戏结束，显示恭喜信息，并提供开始新游戏的按钮。",
                $"{GameImagePrefix}/guessing-numbers.png", $"{GameUrlPrefix}guessing-numbers"),
            new(GameKind.Tictactoe, "井字棋游戏",
                "一个简单的井字棋游戏，玩家可以点击棋盘上的方格来下棋。游戏会检查是否有玩家获胜或者平局，并在游戏结束时显示相应的消息。玩家可以点击“开始新游戏”按钮来重新开始游戏。",
                $"{GameImagePrefix}/tictactoe.png", $"{GameUrlPrefix}tictactoe"),
        };

    public static ToolItem? ToolItem(this ToolKind kind)
    {
        return ToolItems.FirstOrDefault(toolItem => toolItem.Kind == kind);
    }

    public static GameItem? GameItem(this GameKind kind)
    {
        return GameItems.FirstOrDefault(gameItem => gameItem.Kind == kind);
    }
}