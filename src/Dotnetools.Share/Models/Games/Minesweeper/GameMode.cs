namespace Dotnetools.Share.Models.Games.Minesweeper;

public interface IGameMode
{
    int Height { get; }
    int Width { get; }
    int Mines { get; }
}

public class BeginnerMode : IGameMode
{
    public int Height => 9;
    public int Width => 9;
    public int Mines => 10;
}

public class IntermediateMode : IGameMode
{
    public int Height => 16;
    public int Width => 16;
    public int Mines => 40;
}

public class ExpertMode : IGameMode
{
    public int Height => 16;
    public int Width => 30;
    public int Mines => 99;
}

public class CustomMode : IGameMode
{
    public int Height { get; }
    public int Width { get; }
    public int Mines { get; }

    public CustomMode(int height, int width, int mines)
    {
        Height = height;
        Width = width;
        Mines = mines;
    }
}