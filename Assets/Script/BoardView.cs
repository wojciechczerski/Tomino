using UnityEngine;
using Tomino;
using System.Collections.Generic;
using System;

public class BoardView : MonoBehaviour
{
    public GameObject blockPrefab;
    public Sprite blockSprite;
    public Sprite borderBlockSprite;
    public Color borderBlockColor;

    Board gameBoard;
    int renderedBoardHash = -1;
    GameObjectPool<BlockView> blockViewPool;

    public void SetBoard(Board board)
    {
        gameBoard = board;
        int size = board.width * board.height + 10;
        blockViewPool = new GameObjectPool<BlockView>(blockPrefab, size, gameObject);
    }

    public void RenderGameBoard()
    {
        blockViewPool.DeactivateAll();

        foreach (var block in gameBoard.Blocks)
        {
            RenderBlock(blockSprite, block.Type.GetColor(), block.Position);
        }

        foreach (var position in gameBoard.GetPieceShadow())
        {
            RenderBlock(borderBlockSprite, borderBlockColor, position);
        }
    }

    void RenderBlock(Sprite sprite, Color color, Position position)
    {
        var view = blockViewPool.GetAndActivate();
        view.SetSprite(sprite);
        view.SetColor(color);
        view.SetSize(BlockSize());
        view.SetPosition(BlockPosition(position.Row, position.Column));
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
}
