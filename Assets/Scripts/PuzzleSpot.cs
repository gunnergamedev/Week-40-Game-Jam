using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSpot : MonoBehaviour
{
    [SerializeField] private int puzzleNumber;
    public bool correctBlock;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pushable")
        {
            PuzzleBlock block = other.GetComponent<PuzzleBlock>();

            if (puzzleNumber == block.blockNumber)
            {
                correctBlock = true;
            }
            else
            {
                correctBlock = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Pushable")
        {
            correctBlock = false;
        }
    }

}
