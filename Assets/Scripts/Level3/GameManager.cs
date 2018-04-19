using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    private Piece[] pieces;
    private int correctPieceCount;
    private static string FindSceneObjectsOfTag;

    private void Start()
    {
        pieces = FindObjectsOfType<Piece>();
    }

    public void CheckTiles()
    {
        foreach (Piece Piece in pieces)
        {
            if (Piece.values[0] == 1)
            {
                Debug.Log(pieces + " is right");
                correctPieceCount++;
                //Play musics 
            }
        }

        if (correctPieceCount == pieces.Length)
        {
            //WIN CODE
            Debug.Log("cheese");
        }
    }
}