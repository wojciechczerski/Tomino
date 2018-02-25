using UnityEngine;
using Tomino;

public class BoardView : MonoBehaviour
{
    public GameObject blockPrefab;
    Board gameBoard;

    public void RenderGameBoard(Board gameBoard)
    {
        for (int i = transform.childCount - 1; i >= 0; --i)
        {
            Object.Destroy(transform.GetChild(i).gameObject);
        }

        this.gameBoard = gameBoard;

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
            blockObject.transform.localPosition = BlockPosition(block.position.row, block.position.column);
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

    UnityEngine.Color BlockColor(Block block)
    {
        switch (block.color)
        {
            case Tomino.Color.Red: return UnityEngine.Color.red;
            case Tomino.Color.Green: return UnityEngine.Color.green;
            case Tomino.Color.Blue: return UnityEngine.Color.blue;
            case Tomino.Color.Yellow: return UnityEngine.Color.yellow;
            case Tomino.Color.Purple: return UnityEngine.Color.cyan;
            case Tomino.Color.Orange: return UnityEngine.Color.gray;
        }
        return UnityEngine.Color.black;
    }
}
