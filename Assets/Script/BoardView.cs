using UnityEngine;
using Tomino;

public class BoardView : MonoBehaviour
{
    public GameObject blockPrefab;
    public Board gameBoard;
    int renderedBoardHash = -1;

    public void RenderGameBoard()
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
        {
            Object.Destroy(transform.GetChild(i).gameObject);
        }

        foreach (var block in gameBoard.blocks)
        {
            var blockObject = Object.Instantiate(blockPrefab);
            var spriteRenderer = blockObject.GetComponent<SpriteRenderer>();
            var sprite = spriteRenderer.sprite;

            spriteRenderer.color = BlockColor(block);

            blockObject.transform.SetParent(transform);
            blockObject.transform.localPosition = Vector3.zero;

            var scale = sprite.pixelsPerUnit / sprite.rect.width * BlockSize();

            blockObject.transform.localScale = new Vector3(scale, scale);
            blockObject.transform.localPosition = BlockPosition(block.Position.row, block.Position.column);
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

    float BlockSize()
    {
        var boardWidth = GetComponent<RectTransform>().rect.size.x;
        return boardWidth / gameBoard.width;
    }

    Color BlockColor(Block block)
    {
        switch (block.type)
        {
            case Piece.Type.I: return Color.red;
            case Piece.Type.J: return Color.green;
            case Piece.Type.L: return Color.blue;
            case Piece.Type.O: return Color.yellow;
            case Piece.Type.S: return Color.cyan;
            case Piece.Type.T: return Color.gray;
            case Piece.Type.Z: return Color.magenta;
        }
        return Color.black;
    }
}
