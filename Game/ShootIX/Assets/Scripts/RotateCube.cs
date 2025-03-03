using UnityEngine;
using System.Collections.Generic;

public enum Direction
{
    UP,
    DOWN,
    LEFT,
    RIGHT
}

public enum RowColumn
{
    FIRST_ROW,
    SECOND_ROW,
    THIRD_ROW,
    FIRST_COLUMN,
    SECOND_COLUMN,
    THIRD_COLUMN
}

public class RotateCube : MonoBehaviour
{
    [SerializeField] private Direction direction;
    [SerializeField] private RowColumn rowColumn;
    
    private void OnMouseDown()
    {
        Face currentFace = MainFace.Instance.GetFace();

        // Make a copy of the original face's colors
        List<Color> initialColors = new List<Color>();
        for (int i = 0; i < 3; i++)
        {
            initialColors.Add(GetCurrentPieces(currentFace)[i].GetColor());
        }

        // Rotate the cube
        for (int i = 0; i < 3; i++)
        {
            // Get target face to apply to the current face
            Face nextFace = currentFace;
            switch (direction)
            {
                case Direction.UP:
                    nextFace = currentFace.faceToBottom;
                    break;
                case Direction.DOWN:
                    nextFace = currentFace.faceToTop;
                    break;
                case Direction.LEFT:
                    nextFace = currentFace.faceToRight;
                    break;
                case Direction.RIGHT:
                    nextFace = currentFace.faceToLeft;
                    break;
            }
            
            //Debug.Log("Current Face: " + currentFace + ", Next Face: " + nextFace);
            Rotate(currentFace, GetCurrentPieces(nextFace));
            currentFace = nextFace;
        }
        Rotate(currentFace, initialColors);
    }

    private void Rotate(Face firstFace, List<Piece> nextFacePieces)
    {
        List<Piece> firstFacePieces = GetCurrentPieces(firstFace);

        for (int i = 0; i < 3; i++)
        {
            firstFacePieces[i].ChangeColor(nextFacePieces[i].GetColor());
        }
    }

    private void Rotate(Face firstFace, List<Color> nextFaceColors)
    {
        List<Piece> firstFacePieces = GetCurrentPieces(firstFace);

        for (int i = 0; i < 3; i++)
        {
            firstFacePieces[i].ChangeColor(nextFaceColors[i]);
        }
    }

    private List<Piece> GetCurrentPieces(Face face)
    {
        switch (rowColumn)
        {
            case RowColumn.FIRST_ROW:
                return face.firstRow;
            case RowColumn.SECOND_ROW:
                return face.secondRow;
            case RowColumn.THIRD_ROW:
                return face.thirdRow;
            case RowColumn.FIRST_COLUMN:
                return face.firstColumn;
            case RowColumn.SECOND_COLUMN:
                return face.secondColumn;
            case RowColumn.THIRD_COLUMN:
                return face.thirdColumn;
            default:
                return new List<Piece>();
        }
    }
}
