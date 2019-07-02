using System.Collections.Generic;
using Tomino;

public static class BoardExtension
{
    public static Dictionary<Block, Position> GetBlockPositions(this Board board)
    {
        var positions = new Dictionary<Block, Position>();
        foreach (Block block in board.Blocks)
        {
            positions[block] = block.Position;
        }
        return positions;
    }

    public static void AddFullRows(this Board board, int count)
    {
        for (int row = 0; row < count; ++row)
        {
            for (int column = 0; column < board.width; ++column)
            {
                var position = new Position(row, column);
                var allPositions = new List<Position>(board.Blocks.Map(b => b.Position));

                if (!allPositions.Contains(position))
                {
                    board.Blocks.Add(new Block(position, PieceType.I));
                }
            }
        }
    }

    public static int FallDistance(this Board board)
    {
        return board.piece.GetPositions().Values.Map(p => p.Row).Min();
    }
}

