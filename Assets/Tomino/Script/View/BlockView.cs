using UnityEngine;

public class BlockView : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    internal void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void SetPosition(Vector3 position)
    {
        transform.localPosition = position;
    }

    public void SetSize(float size)
    {
        var sprite = spriteRenderer.sprite;
        var scale = sprite.pixelsPerUnit / sprite.rect.width * size;
        transform.localScale = new Vector3(scale, scale);
    }
}
