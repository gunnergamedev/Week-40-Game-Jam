using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour
{


    public int[] values = { 1, 0, 0, 0 };
    public float speed;
    float realRotation;

    private Piece[] pieces;
    private int correctPieceCount;

    public GameManager gm;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {


        if (transform.eulerAngles.z != realRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, realRotation), speed);
        }
        gm.CheckTiles();

    }



    void OnMouseDown()
    {

        RotatePiece();
        gm.CheckTiles();

    }

    public void RotatePiece()
    {
        realRotation += 90;

        if (realRotation == 360)
        {
            realRotation = 0;
        }

        RotateValues();
    }



    public void RotateValues()
    {

        int temp = values[0];

        for (int i = 0; i < values.Length - 1; i++)
        {
            values[i] = values[i + 1];
        }
        values[3] = temp;

        gm.CheckTiles();
    }

    public void CheckTiles()
    {
        foreach (Piece piece in pieces)
        {
            if (piece.values[0] == 1)
            {
                Debug.Log(piece.name + " is right");
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