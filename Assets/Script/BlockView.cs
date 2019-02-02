using UnityEngine;

public class BlockView : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
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
