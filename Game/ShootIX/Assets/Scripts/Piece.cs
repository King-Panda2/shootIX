using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private Color color = Color.white;
    private Face face;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        face = transform.parent.gameObject.GetComponent<Face>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = color;
    }

    private void AddToCenterFace()
    {

    }
}
