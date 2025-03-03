using UnityEngine;
using System.Collections.Generic;

public class Face : MonoBehaviour
{
    // Lists of pieces, organized into rows and columns
    [SerializeField] private List<Piece> firstRow;
    [SerializeField] private List<Piece> secondRow;
    [SerializeField] private List<Piece> thirdRow;
    [SerializeField] private List<Piece> firstColumn;
    [SerializeField] private List<Piece> secondColumn;
    [SerializeField] private List<Piece> thirdColumn;

    // Relation to other faces
    [SerializeField] private Face faceToRight;
    [SerializeField] private Face faceToLeft;
    [SerializeField] private Face faceToTop;
    [SerializeField] private Face faceToBottom;

    public void RotateRight()
    {

    }
    
    public void RotateLeft()
    {

    }

    public void RotateUp()
    {

    }

    public void RotateDown()
    {

    }
}
