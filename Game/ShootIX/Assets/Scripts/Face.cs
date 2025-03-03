using UnityEngine;
using System.Collections.Generic;

public class Face : MonoBehaviour
{
    // Lists of pieces, organized into rows and columns
    public List<Piece> firstRow;
    public List<Piece> secondRow;
    public List<Piece> thirdRow;
    public List<Piece> firstColumn;
    public List<Piece> secondColumn;
    public List<Piece> thirdColumn;

    // Relation to other faces
    public Face faceToRight;
    public Face faceToLeft;
    public Face faceToTop;
    public Face faceToBottom;
}
