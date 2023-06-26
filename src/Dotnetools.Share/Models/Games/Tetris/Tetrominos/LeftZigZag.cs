using Dotnetools.Share.Models.Games.Tetris.Enums;

namespace Dotnetools.Share.Models.Games.Tetris.Tetrominos;

/// <summary>
/// A left-zigzag tetromino
/// X X        X
///   X X    X X
///          X
/// </summary>
public class LeftZigZag : Tetromino
{
    public LeftZigZag(Grid grid) : base(grid) { }

    public override TetrominoStyle Style => TetrominoStyle.LeftZigZag;

    public override string CssClass => "tetris-red-cell";

    public override CellCollection CoveredCells
    {
        get
        {
            CellCollection cells = new CellCollection();
            cells.Add(CenterPieceRow, CenterPieceColumn);

            switch (Orientation)
            {
                case TetrominoOrientation.LeftRight:
                    cells.Add(CenterPieceRow + 1, CenterPieceColumn);
                    cells.Add(CenterPieceRow + 1, CenterPieceColumn - 1);
                    cells.Add(CenterPieceRow, CenterPieceColumn + 1);
                    break;

                case TetrominoOrientation.DownUp:
                    cells.Add(CenterPieceRow - 1, CenterPieceColumn);
                    cells.Add(CenterPieceRow, CenterPieceColumn + 1);
                    cells.Add(CenterPieceRow + 1, CenterPieceColumn + 1);
                    break;

                case TetrominoOrientation.RightLeft:
                    cells.Add(CenterPieceRow, CenterPieceColumn - 1);
                    cells.Add(CenterPieceRow - 1, CenterPieceColumn);
                    cells.Add(CenterPieceRow - 1, CenterPieceColumn + 1);
                    break;

                case TetrominoOrientation.UpDown:
                    cells.Add(CenterPieceRow + 1, CenterPieceColumn);
                    cells.Add(CenterPieceRow, CenterPieceColumn - 1);
                    cells.Add(CenterPieceRow - 1, CenterPieceColumn - 1);
                    break;
            }
            return cells;
        }
    }
}