using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField] private Color color = Color.white;
    private Face face;

    void Start()
    {
        face = transform.parent.gameObject.GetComponent<Face>();
    }

    private void AddToCenterFace()
    {

    }
}
