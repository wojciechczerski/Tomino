using Tomino;
using System.Collections.Generic;

public static class PieceRotations
{
    private static int Row(int blockIndex, string[] config)
    {
        for (int row = 0; row < config.Length; ++row)
        {
            if (config[row].Contains(blockIndex.ToString()))
            {
                return row;
            }
        }
        return -1;
    }

    private static int Column(int blockIndex, string[] config)
    {
        foreach (var row in config)
        {
            if (row.Contains(blockIndex.ToString()))
            {
                return row.IndexOf(blockIndex.ToString()) / 3;
            }
        }
        return -1;
    }

    private static Position[][] PositionsFromVisualFormat(string[][] format)
    {
        var positions = new List<Position[]>();

        foreach (var rotationFormat in format)
        {
            var firstRow = Row(0, rotationFormat);
            var firstCol = Column(0, rotationFormat);

            var rotation = new List<Position>();
            rotation.Add(new Position(0, 0));

            for (int blockIndex = 1; blockIndex < 4; ++blockIndex)
            {
                var row = Row(blockIndex, rotationFormat);
                var column = Column(blockIndex, rotationFormat);
                rotation.Add(new Position(firstRow - row, column - firstCol));
            }

            positions.Add(rotation.ToArray());
        }

        return positions.ToArray();
    }

    public static Position[][] SPiece()
    {
        return PositionsFromVisualFormat(new string[][]
        {
            new string[]
            {
                "   [0][1]",
                "[3][2]",
            },
            new string[]
            {
                "[3]",
                "[2][0]",
                "   [1]",
            },
            new string[]
            {
                "   [2][3]",
                "[1][0]",
            },
        });
    }

    public static Position[][] ZPiece()
    {
        return PositionsFromVisualFormat(new string[][]
        {
            new string[]
            {
                "[1][0]",
                "   [2][3]",
            },
            new string[]
            {
                "   [1]",
                "[2][0]",
                "[3]",
            },
            new string[]
            {
                "[3][2]",
                "   [0][1]",
            },
        });
    }

    public static Position[][] OPiece()
    {
        return PositionsFromVisualFormat(new string[][]
        {
            new string[]
            {
                "[1][2]",
                "[0][3]",
            },
            new string[]
            {
                "[1][2]",
                "[0][3]",
            },
            new string[]
            {
                "[1][2]",
                "[0][3]",
            },
        });
    }

    public static Position[][] TPiece()
    {
        return PositionsFromVisualFormat(new string[][]
        {
            new string[]
            {
                "[2][0][1]",
                "   [3]   ",
            },
            new string[]
            {
                "   [2]",
                "[3][0]",
                "   [1]",
            },
            new string[]
            {
                "   [3]   ",
                "[1][0][2]",
            }
        });
    }

    public static Position[][] IPiece()
    {
        return PositionsFromVisualFormat(new string[][]
        {
            new string[]
            {
                "[3][2][0][1]",
            },
            new string[]
            {
                "[3]",
                "[2]",
                "[0]",
                "[1]",
            },
            new string[]
            {
                "[1][0][2][3]",
            }
        });
    }

    public static Position[][] JPiece()
    {
        return PositionsFromVisualFormat(new string[][]
        {
            new string[]
            {
                "[3]",
                "[2][0][1]",
            },
            new string[]
            {
                "[2][3]",
                "[0]",
                "[1]",
            },
            new string[]
            {
                "[1][0][2]",
                "      [3]",
            },
        });
    }

    public static Position[][] LPiece()
    {
        return PositionsFromVisualFormat(new string[][]
        {
            new string[]
            {
                "[2][0][1]",
                "[3]",
            },
            new string[]
            {
                "[3][2]",
                "   [0]",
                "   [1]",
            },
            new string[]
            {
                "      [3]",
                "[1][0][2]",
            },
        });
    }
}
