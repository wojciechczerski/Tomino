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
                var min = blocks.Select(block => block.position.column).Min();
                var max = blocks.Select(block => block.position.column).Max();
                return Math.Abs(max - min);
            }
        }

        public int Top
        {
            get
            {
                return blocks.Select(block => block.position.row).Max();
            }
        }

        public Piece(Position[] blockPositions, Type type)
        {
            blocks = blockPositions.Select(position => new Block(position, type)).ToArray();
        }

        public Dictionary<Block, Position> GetPositions()
        {
            var positions = new Dictionary<Block, Position>();
            foreach (Block block in blocks)
            {
                positions[block] = block.position;
            }
            return positions;
        }

        public void Move(int rowOffset, int columnOffset)
        {
            foreach (var block in blocks)
            {
                block.position.column += columnOffset;
                block.position.row += rowOffset;
            }
        }

        public void MoveLeft()
        {
            Move(0, -1);
        }

        public void MoveRight()
        {
            Move(0, 1);
        }

        public void MoveDown()
        {
            Move(-1, 0);
        }

        virtual public void Rotate()
        {
            var offset = blocks[0].position;

            foreach (var block in blocks)
            {
                var position = new Position()
                {
                    row = block.position.row - offset.row,
                    column = block.position.column - offset.column
                };

                block.position = new Position()
                {
                    row = -position.column + offset.row,
                    column = position.row + offset.column
                };
            }
        }
    }
}
