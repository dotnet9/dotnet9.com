using Dotnetools.Share.Models.Games.Tetris.Enums;

namespace Dotnetools.Share.Models.Games.Tetris;

public class Grid
{
    public int Width { get; } = 10;
    public int Height { get; } = 20;
    public CellCollection Cells { get; set; } = new CellCollection();

    public GameState State { get; set; } = GameState.NotStarted;

    public bool IsStarted
    {
        get
        {
            return State == GameState.Playing
                   || State == GameState.GameOver;
        }
    }
}