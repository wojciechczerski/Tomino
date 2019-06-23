using System.Linq;
using System.Collections.Generic;
using System;

namespace Tomino
{
    public class Piece
    {
        public Block[] blocks;
        public readonly bool canRotate;

        public int Width
        {
            get
            {
                var min = blocks.Select(block => block.Position.Column).Min();
                var max = blocks.Select(block => block.Position.Column).Max();
                return Math.Abs(max - min);
            }
        }

        public int Top => blocks.Select(block => block.Position.Row).Max();

        public Piece(Position[] blockPositions, PieceType type, bool canRotate = true)
        {
            blocks = blockPositions.Select(position => new Block(position, type)).ToArray();
            this.canRotate = canRotate;
        }

        public Dictionary<Block, Position> GetPositions()
        {
            var positions = new Dictionary<Block, Position>();
            foreach (Block block in blocks)
            {
                positions[block] = block.Position;
            }
            return positions;
        }
    }
}
