using UnityEngine;
using Tomino;

public class PieceView : MonoBehaviour
{
    public GameObject blockPrefab;
    public Sprite blockSprite;
    public RectTransform container;

    private Board board;
    GameObjectPool<BlockView> blockViewPool;
    private PieceType? renderedPieceType;
    private int blockPoolSize = 10;

    public void SetBoard(Board board)
    {
        this.board = board;
        blockViewPool = new GameObjectPool<BlockView>(blockPrefab, blockPoolSize, gameObject);
    }

    void Update()
    {
        if (renderedPieceType == null || board.nextPiece.Type != renderedPieceType)
        {
            RenderPiece(board.nextPiece);
            renderedPieceType = board.nextPiece.Type;
        }
    }

    void RenderPiece(Piece piece)
    {
        blockViewPool.DeactivateAll();

        var blockSize = BlockSize(piece);

        foreach (var block in piece.blocks)
        {
            var blockView = blockViewPool.GetAndActivate();
            blockView.SetSprite(blockSprite);
            blockView.SetColor(block.Type.GetColor());
            blockView.SetSize(blockSize);
            blockView.SetPosition(BlockPosition(block.Position, blockSize));
        }

        var pieceBlocks = blockViewPool.Items.First(piece.blocks.Length);
        var xValues = pieceBlocks.Map(b => b.transform.localPosition.x);
        var yValues = pieceBlocks.Map(b => b.transform.localPosition.y);

        var halfBlockSize = blockSize / 2.0f;
        var minX = Mathf.Min(xValues) - halfBlockSize;
        var maxX = Mathf.Max(xValues) + halfBlockSize;
        var minY = Mathf.Min(yValues) - halfBlockSize;
        var maxY = Mathf.Max(yValues) + halfBlockSize;
        var width = maxX - minX;
        var height = maxY - minY;
        var offsetX = (-width / 2.0f) - minX;
        var offsetY = (-height / 2.0f) - minY;

        foreach (var block in pieceBlocks)
        {
            block.transform.localPosition += new Vector3(offsetX, offsetY);
        }
    }

    Vector3 BlockPosition(Position position, float blockSize)
    {
        return new Vector3(position.Column * blockSize, position.Row * blockSize);
    }

    public float BlockSize(Piece piece)
    {
        var width = container.rect.size.x;
        return width / piece.blocks.Length;
    }
}
