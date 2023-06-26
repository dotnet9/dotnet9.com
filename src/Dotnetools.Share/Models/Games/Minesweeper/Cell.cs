namespace Dotnetools.Share.Models.Games.Minesweeper;

public class Cell
{
    public Guid ID { get; }
    public int X { get; }
    public int Y { get; }

    public bool IsMine { get; private set; }
    public bool IsFlagged { get; private set; }
    public bool IsRevealed { get; private set; }
    public bool IsDeathMine { get; private set; }
    public int AdjacentMines { get; private set; }

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
        ID = Guid.NewGuid();
    }

    public void Flag()
    {
        if (IsRevealed == false)
        {
            IsFlagged = !IsFlagged;
        }
    }

    public void Reveal()
    {
        IsRevealed = true;
        IsFlagged = false;
    }

    public Cell WithIsMine(bool value)
    {
        IsMine = value;
        return this;
    }

    public Cell WithIsFlag(bool value)
    {
        IsFlagged = value;
        return this;
    }

    public Cell WithIsDeathMine(bool value)
    {
        IsDeathMine = value;
        return this;
    }


    public Cell WithIsRevealed(bool value)
    {
        IsRevealed = value;
        return this;
    }

    public Cell WithAdjacentMines(int value)
    {
        AdjacentMines = value;
        return this;
    }

    public IEnumerable<Cell> ToList()
    {
        yield return this;
    }
}