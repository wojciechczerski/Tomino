using System.Collections.Generic;
using System;

namespace Tomino
{
    public class Piece
    {
        public Block[] blocks;
        public readonly bool canRotate;
        public PieceType Type { get; private set; }

        public int Width
        {
            get
            {
                var min = blocks.Map(block => block.Position.Column).Min();
                var max = blocks.Map(block => block.Position.Column).Max();
                return Math.Abs(max - min);
            }
        }

        public int Top => blocks.Map(block => block.Position.Row).Max();

        public Piece(Position[] blockPositions, PieceType type, bool canRotate = true)
        {
            blocks = blockPositions.Map(position => new Block(position, type));
            Type = type;
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
