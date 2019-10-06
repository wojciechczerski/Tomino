using UnityEngine;
using Tomino;
using System.Collections.Generic;
using System;

public class BoardView : MonoBehaviour
{
    public GameObject blockPrefab;
    public Sprite[] blockSprites;
    public Sprite shadowBlockSprite;

    Board gameBoard;
    int renderedBoardHash = -1;
    bool forceRender = false;
    GameObjectPool<BlockView> blockViewPool;
    RectTransform rectTransform;

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
            RenderBlock(BlockSprite(block.Type), block.Position);
        }

        foreach (var position in gameBoard.GetPieceShadow())
        {
            RenderBlock(shadowBlockSprite, position);
        }
    }

    void RenderBlock(Sprite sprite, Position position)
    {
        var view = blockViewPool.GetAndActivate();
        view.SetSprite(sprite);
        view.SetSize(BlockSize());
        view.SetPosition(BlockPosition(position.Row, position.Column));
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        var hash = gameBoard.GetHashCode();
        if (forceRender || hash != renderedBoardHash)
        {
            RenderGameBoard();
            renderedBoardHash = hash;
            forceRender = false;
        }
    }

    void OnRectTransformDimensionsChange()
    {
        forceRender = true;
    }

    Vector3 BlockPosition(int row, int column)
    {
        var size = BlockSize();
        var position = new Vector3(column * size, row * size);
        var offset = new Vector3(size / 2, size / 2, 0);
        return position + offset - PivotOffset();
    }

    public float BlockSize()
    {
        var boardWidth = rectTransform.rect.size.x;
        return boardWidth / gameBoard.width;
    }

    public Sprite BlockSprite(PieceType type)
    {
        return blockSprites[(int)type];
    }

    public Vector3 PivotOffset()
    {
        var pivot = rectTransform.pivot;
        var boardSize = rectTransform.rect.size;
        return new Vector3(boardSize.x * pivot.x, boardSize.y * pivot.y);
    }
}
