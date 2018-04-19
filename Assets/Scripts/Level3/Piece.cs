using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour
{
    public int[] values = { 1, 2, 3, 4 };
    public float speed;
    private float realRotation;

    public bool isPuzzleSolved;

    private PuzzleThreeManager puzzleManager;

    private void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleThreeManager>();
        isPuzzleSolved = true; //so can't rotate pieces before pushing button
    }

    public void ShufflePieces()
    {
        switch(values[0])
        {
            case 1:
                realRotation = 0;
                break;

            case 2:
                realRotation = 90;
                break;

            case 3:
                realRotation = 180;
                break;

            case 4:
                realRotation = 270;
                break;
        }
    }

    private void Update()
    {
        if (!isPuzzleSolved)
        {
            if (transform.eulerAngles.z != realRotation)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, realRotation), speed);
            }
        }
    }

    private void OnMouseDown()
    {
        if (!isPuzzleSolved)
        {
            RotatePiece();
        }
    }

    private void RotatePiece()
    {
        realRotation += 90;

        if (realRotation == 360)
        {
            realRotation = 0;
        }

        RotateValues();
    }

    private void RotateValues()
    {
        int temp = values[0];

        for (int i = 0; i < values.Length - 1; i++)
        {
            values[i] = values[i + 1];
        }
        values[3] = temp;

        CheckPiecesDelay();
    }

    private void CheckPiecesDelay()
    {
        StartCoroutine(CheckPieces());
    }

    private IEnumerator CheckPieces()
    {
        yield return new WaitForSeconds(0.2f);
        puzzleManager.CheckIfAllPiecesCorrect();
    }
}