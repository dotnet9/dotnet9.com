namespace Dotnet9.BlazorWeb.Pages.Games.Models.Minesweeper;

public class GameBoard
{
    public IGameMode PlayMode { get; private set; } = new IntermediateMode();
    public IEnumerable<Cell> Grid { get; private set; }

    public GameStatus Status { get; private set; }
    public TimeManager Timer { get; private set; }

    public int MinesNotFlagged
        => PlayMode.Mines - Grid.Where(cell => cell.IsFlagged).Count();

    public GameBoard()
    {
        Initialize();
    }

    public void Initialize()
    {
        Initialize(PlayMode);
    }

    public void Initialize(IGameMode mode)
    {
        PlayMode = mode;

        var grid = new List<Cell>();

        for (int x = 1; x <= mode.Width; x++)
        {
            for (int y = 1; y <= mode.Height; y++)
            {
                grid.Add(new Cell(x, y));
            }
        }

        Grid = grid;
        Status = GameStatus.AwaitingFirstMove;
        Timer = new TimeManager();
    }

    public void FirstMove(int x, int y)
    {
        var neighbors = GetNeighbors(x, y);
        var currentCell = GetCell(x, y);
        var unavailables = neighbors.Union(currentCell.ToList());

        var mineCells = Grid
            .Except(unavailables)
            .OrderBy(_ => Guid.NewGuid())
            .Take(PlayMode.Mines);

        foreach (var cell in mineCells)
        {
            var mineCell = GetCell(cell.X, cell.Y);
            mineCell.WithIsMine(true);
        }

        foreach (var noMineCell in Grid.Where(cell => cell.IsMine == false))
        {
            var adjacentCells = GetNeighbors(noMineCell.X, noMineCell.Y);
            var adjacentMines = adjacentCells.Count(cell => cell.IsMine);
            noMineCell.WithAdjacentMines(adjacentMines);
        }

        Timer.Start();
        Status = GameStatus.InProgress;
    }

    public void MakeMove(int x, int y)
    {
        if (IsInvalidMove())
        {
            return;
        }

        if (Status == GameStatus.AwaitingFirstMove)
        {
            FirstMove(x, y);
        }

        RevealCell(x, y);
    }

    public void FlagCell(int x, int y)
    {
        if (IsInvalidMove())
        {
            return;
        }

        var currentCell = GetCell(x, y);

        currentCell.Flag();
        CheckForCompletion();
    }

    private void RevealCell(int x, int y)
    {
        var currentCell = GetCell(x, y);
        currentCell.Reveal();

        if (currentCell.IsMine)
        {
            Timer.Stop();
            Status = GameStatus.Defeated;
            RevealAllMines();
            currentCell.WithIsDeathMine(true);
        }
        else
        {
            if (currentCell.AdjacentMines == 0)
            {
                RevealZeros(x, y);
            }

            CheckForCompletion();
        }
    }

    private void RevealAllMines()
    {
        Grid.Where(cell => cell.IsMine)
            .ToList()
            .ForEach(mineCell => mineCell.WithIsRevealed(true));
    }

    private void RevealZeros(int x, int y)
    {
        var neighborCells = GetNeighbors(x, y)
            .Where(cell => cell.IsRevealed == false);

        foreach (var neighbor in neighborCells)
        {
            neighbor.WithIsRevealed(true);

            if (neighbor.AdjacentMines == 0)
            {
                RevealZeros(neighbor.X, neighbor.Y);
            }
        }
    }

    private void CheckForCompletion()
    {
        var unrevealedCells = Grid
            .Where(cell => cell.IsRevealed == false)
            .Select(cell => cell.ID);

        var mineCells = Grid
            .Where(cell => cell.IsMine == true)
            .Select(cell => cell.ID);

        var existUnrevealedCellsWithoutMines = unrevealedCells.Except(mineCells).Any();

        if (existUnrevealedCellsWithoutMines == false)
        {
            Status = GameStatus.Victory;
            FlagAllMinesByID(mineCells);
            Timer.Stop();
        }
    }

    private void FlagAllMinesByID(IEnumerable<Guid> cellIDs)
    {
        foreach (var guid in cellIDs)
        {
            var mineCell = GetCell(guid);
            mineCell.WithIsFlag(true);
        }
    }

    private IEnumerable<Cell> GetNeighbors(int x, int y)
    {
        var adjacentCells = Grid
            .Where(cell => cell.X >= x - 1)
            .Where(cell => cell.X <= x + 1)
            .Where(cell => cell.Y >= y - 1)
            .Where(cell => cell.Y <= y + 1);

        var currentCell = GetCell(x, y);

        return adjacentCells.Except(currentCell.ToList());
    }

    private Cell GetCell(int x, int y)
    {
        return Grid
            .Where(cell => cell.X == x)
            .Where(cell => cell.Y == y)
            .FirstOrDefault();
    }

    private Cell GetCell(Guid id)
    {
        return Grid
            .Where(cell => cell.ID == id)
            .FirstOrDefault();
    }

    private bool IsInvalidMove()
    {
        if (Status == GameStatus.Victory || Status == GameStatus.Defeated)
        {
            return true;
        }
        //if (MinesNotFlagged == 0)
        //{
        //    return true;
        //}

        return false;
    }
}