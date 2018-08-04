using System.Collections.Generic;
using Tomino;

public static class BoardExtension
{
    public static Dictionary<Block, Position> GetBlockPositions(this Board board)
    {
        var positions = new Dictionary<Block, Position>();
        foreach (Block block in board.blocks)
        {
            positions[block] = block.position;
        }
        return positions;
    }
}

