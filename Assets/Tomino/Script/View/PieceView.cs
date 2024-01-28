using Tomino.Model;
using Tomino.Shared;
using UnityEngine;

namespace Tomino.View
{
    public class PieceView : MonoBehaviour
    {
        public GameObject blockPrefab;
        public Sprite blockSprite;
        public ThemeProvider themeProvider;
        public RectTransform container;

        private Board _board;
        private GameObjectPool<BlockView> _blockViewPool;
        private PieceType? _renderedPieceType;
        private const int BlockPoolSize = 10;
        private bool _forceRender;

        private void Awake()
        {
            Settings.changedEvent += () => _forceRender = true;
        }

        public void SetBoard(Board board)
        {
            _board = board;
            _blockViewPool = new GameObjectPool<BlockView>(blockPrefab, BlockPoolSize, gameObject);
        }

        internal void Update()
        {
            if (_renderedPieceType != null && !_forceRender && _board.NextPiece.Type == _renderedPieceType) return;

            RenderPiece(_board.NextPiece);
            _renderedPieceType = _board.NextPiece.Type;
            _forceRender = false;
        }

        internal void OnRectTransformDimensionsChange()
        {
            _forceRender = true;
        }

        private void RenderPiece(Piece piece)
        {
            _blockViewPool.DeactivateAll();

            var blockSize = BlockSize(piece);

            foreach (var block in piece.blocks)
            {
                var blockView = _blockViewPool.GetAndActivate();
                blockView.SetSprite(blockSprite);
                blockView.SetSize(blockSize);
                blockView.SetColor(BlockColor(block.Type));
                blockView.SetPosition(BlockPosition(block.Position, blockSize));
            }

            var pieceBlocks = _blockViewPool.Items.First(piece.blocks.Length);
            var xValues = pieceBlocks.Map(b => b.transform.localPosition.x);
            var yValues = pieceBlocks.Map(b => b.transform.localPosition.y);

            var halfBlockSize = blockSize / 2.0f;
            var minX = Mathf.Min(xValues) - halfBlockSize;
            var maxX = Mathf.Max(xValues) + halfBlockSize;
            var minY = Mathf.Min(yValues) - halfBlockSize;
            var maxY = Mathf.Max(yValues) + halfBlockSize;
            var width = maxX - minX;
            var height = maxY - minY;
            var offsetX = -width / 2.0f - minX;
            var offsetY = -height / 2.0f - minY;

            foreach (var block in pieceBlocks)
            {
                block.transform.localPosition += new Vector3(offsetX, offsetY);
            }
        }

        private static Vector3 BlockPosition(Position position, float blockSize)
        {
            return new Vector3(position.Column * blockSize, position.Row * blockSize);
        }

        private float BlockSize(Piece piece)
        {
            var rect = container.rect;
            var width = rect.size.x;
            var height = rect.size.y;
            var numBlocks = piece.blocks.Length;
            return Mathf.Min(width / numBlocks, height / numBlocks);
        }

        private Color BlockColor(PieceType type)
        {
            return themeProvider.currentTheme.BlockColors[(int)type];
        }
    }
}
