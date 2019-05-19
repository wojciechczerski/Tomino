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
    BlockView[] blocks;
    Dictionary<Piece.Type, Color> blockColor = new Dictionary<Piece.Type, Color>();

    public void SetBoard(Board board)
    {
        gameBoard = board;
        CreateBlocksPool(board.width * board.height + 10);
    }

    public void RenderGameBoard()
    {
        for (int i = 0; i < blocks.Length; ++i)
        {
            blocks[i].gameObject.SetActive(false);
        }

        int blockIndex = 0;
        foreach (var block in gameBoard.Blocks)
        {
            var blockView = blocks[blockIndex];
            blockView.gameObject.SetActive(true);

            blockView.SetSprite(blockSprite);
            blockView.SetColor(blockColor[block.Type]);
            blockView.SetSize(BlockSize());
            blockView.SetPosition(BlockPosition(block.Position.Row, block.Position.Column));

            blockIndex++;
        }

        foreach (var position in gameBoard.GetPieceShadow())
        {
            var blockView = blocks[blockIndex];
            blockView.gameObject.SetActive(true);

            blockView.SetSprite(borderBlockSprite);
            blockView.SetColor(borderBlockColor);
            blockView.SetSize(BlockSize());
            blockView.SetPosition(BlockPosition(position.Row, position.Column));

            blockIndex++;
        }
    }

    void CreateBlocksPool(int poolSize)
    {
        blocks = new BlockView[poolSize];
        for (int i = 0; i < poolSize; ++i)
        {
            var newBlock = Instantiate(blockPrefab);
            newBlock.transform.parent = transform;
            newBlock.SetActive(false);
            blocks[i] = newBlock.GetComponent<BlockView>();
        }
    }

    void CreateBlockColorMapping()
    {
        int index = 0;
        foreach (Piece.Type type in Enum.GetValues(typeof(Piece.Type)))
        {
            var color = Color.black;
            ColorUtility.TryParseHtmlString(Constant.ColorPalette.Blocks[index++], out color);
            blockColor.Add(type, color);
        }
    }

    void Awake()
    {
        CreateBlockColorMapping();
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
