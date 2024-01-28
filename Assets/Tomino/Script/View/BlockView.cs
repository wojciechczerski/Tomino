using UnityEngine;

namespace Tomino.View
{
    public class BlockView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        internal void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public void SetPosition(Vector3 position)
        {
            transform.localPosition = position;
        }

        public void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        public void SetSize(float size)
        {
            var sprite = _spriteRenderer.sprite;
            var scale = sprite.pixelsPerUnit / sprite.rect.width * size;
            transform.localScale = new Vector3(scale, scale);
        }
    }
}
