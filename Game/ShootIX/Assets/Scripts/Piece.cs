using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private Color color;
    private Face face;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        face = transform.parent.gameObject.GetComponent<Face>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
    }

    public void ChangeColor(Color newColor)
    {
        color = newColor;
        spriteRenderer.color = newColor;
    }

    public Color GetColor()
    {
        return color;
    }
}
