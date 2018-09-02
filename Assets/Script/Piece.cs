using System.Linq;
using System.Collections.Generic;
using System;

namespace Tomino
{
    public class Piece
    {
        public enum Type
        {
            I, J, L, O, S, T, Z
        }

        public Block[] blocks;

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

        public Piece(Position[] blockPositions, Type type)
        {
            blocks = blockPositions.Select(position => new Block(position, type)).ToArray();
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

        public void Move(int rowOffset, int columnOffset)
        {
            foreach (var block in blocks)
            {
                block.MoveBy(rowOffset, columnOffset);
            }
        }

        public void MoveLeft() => Move(0, -1);

        public void MoveRight() => Move(0, 1);

        public void MoveDown() => Move(-1, 0);

        virtual public void Rotate()
        {
            var offset = blocks[0].Position;

            foreach (var block in blocks)
            {
                var row = block.Position.Row - offset.Row;
                var column = block.Position.Column - offset.Column;
                block.MoveTo(-column + offset.Row, row + offset.Column);
            }
        }
    }
}
