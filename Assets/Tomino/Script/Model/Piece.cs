using System.Collections.Generic;
using System;

namespace Tomino
{
    /// <summary>
    /// A piece is a collection of blocks that all move together on the board.
    /// </summary>
    public class Piece
    {
        /// <summary>
        /// The collection of blocks contained in this piece.
        /// </summary>
        public Block[] blocks;

        /// <summary>
        /// Determines whether the piece can be rotated.
        /// </summary>
        public readonly bool canRotate;

        /// <summary>
        /// The type of the piece.
        /// </summary>
        public PieceType Type { get; private set; }

        /// <summary>
        /// Returns number of columns occupied by this piece.
        /// </summary>
        /// <returns>The width of the piece.</returns>
        public int Width
        {
            get
            {
                var min = blocks.Map(block => block.Position.Column).Min();
                var max = blocks.Map(block => block.Position.Column).Max();
                return Math.Abs(max - min);
            }
        }

        /// <summary>
        /// Returns the topmost row in which a block of the piece is positioned.
        /// </summary>
        /// <param name="block.Position.Row"></param>
        /// <returns>The top row of the piece.</returns>
        public int Top => blocks.Map(block => block.Position.Row).Max();

        /// <summary>
        /// Initializes piece with specified blocks and type.
        /// </summary>
        /// <param name="blockPositions">The collection of blocks the piece should contain.</param>
        /// <param name="type">The type of the piece.</param>
        /// <param name="canRotate">Determines whether the piece can be rotated.</param>
        public Piece(Position[] blockPositions, PieceType type, bool canRotate = true)
        {
            blocks = blockPositions.Map(position => new Block(position, type));
            Type = type;
            this.canRotate = canRotate;
        }

        /// <summary>
        /// Returns a mapping of blocks to the positions of blocks.
        /// </summary>
        /// <returns>The dictionary with block to position mapping.</returns>
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
