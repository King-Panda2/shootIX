using UnityEngine;
using System.Collections.Generic;

public class MainFace : MonoBehaviour
{
    // TO DO: randomization functionality
    
    public static MainFace Instance;

    private Face face;
    private List<Color> bulletWeights;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        face = gameObject.GetComponent<Face>();

        CalculateBulletWeights();
    }

    public void CalculateBulletWeights()
    {
        bulletWeights = new List<Color>();

        // Loop through the pieces on the face and add each of their colors to bulletWeights
        foreach (Piece piece in face.firstRow)
        {
            bulletWeights.Add(piece.GetColor());
        }
        foreach (Piece piece in face.secondRow)
        {
            bulletWeights.Add(piece.GetColor());
        }
        foreach (Piece piece in face.thirdRow)
        {
            bulletWeights.Add(piece.GetColor());
        }
    }

    public List<Color> GetBulletWeights()
    {
        return bulletWeights;
    }

    public Face GetFace()
    {
        return face;
    }
}
