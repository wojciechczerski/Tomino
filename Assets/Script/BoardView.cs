using UnityEngine;
using Tomino;

public class BoardView : MonoBehaviour
{
    public GameObject blockPrefab;
    Board gameBoard;
    int renderedBoardHash = -1;
    GameObject[] blocks;

    public void SetBoard(Board board)
    {
        gameBoard = board;
        CreateBlocksPool(board.width * board.height);
    }

    public void RenderGameBoard()
    {
        for (int i = 0; i < blocks.Length; ++i)
        {
            blocks[i].SetActive(false);
        }

        int blockIndex = 0;
        foreach (var block in gameBoard.Blocks)
        {
            var blockObject = blocks[blockIndex];
            blockObject.SetActive(true);
            var spriteRenderer = blockObject.GetComponent<SpriteRenderer>();
            var sprite = spriteRenderer.sprite;

            spriteRenderer.color = BlockColor(block);

            var scale = sprite.pixelsPerUnit / sprite.rect.width * BlockSize();

            blockObject.transform.localScale = new Vector3(scale, scale);
            blockObject.transform.localPosition = BlockPosition(block.Position.Row, block.Position.Column);

            blockIndex++;
        }
    }

    void CreateBlocksPool(int poolSize)
    {
        blocks = new GameObject[poolSize];
        for (int i = 0; i < poolSize; ++i)
        {
            var newBlock = Instantiate(blockPrefab);
            newBlock.transform.parent = transform;
            newBlock.SetActive(false);
            blocks[i] = newBlock;
        }
    }

    void Update()
    {
        var hash = gameBoard.GetHashCode();
        if (hash != renderedBoardHash)
        {
            RenderGameBoard();
            renderedBoardHash = hash;
        }
    }

    Vector3 BlockPosition(int row, int column)
    {
        var size = BlockSize();
        var position = new Vector3(column * size, row * size);
        var offset = new Vector3(size / 2, size / 2, 0);
        return position + offset;
    }

    public float BlockSize()
    {
        var boardWidth = GetComponent<RectTransform>().rect.size.x;
        return boardWidth / gameBoard.width;
    }

    Color BlockColor(Block block)
    {
        switch (block.Type)
        {
            case Piece.Type.I: return Color.red;
            case Piece.Type.J: return Color.green;
            case Piece.Type.L: return Color.blue;
            case Piece.Type.O: return Color.yellow;
            case Piece.Type.S: return Color.cyan;
            case Piece.Type.T: return Color.gray;
            case Piece.Type.Z: return Color.magenta;
            default: return Color.black;
        }
    }
}
