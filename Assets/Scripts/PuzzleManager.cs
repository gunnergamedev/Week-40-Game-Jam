using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject[] grid;
    [SerializeField] private Vector3[] gridpointPosition;
    [SerializeField] private GameObject gridpointPrefab;

    PuzzleSpot[] puzzleSpots;
    private bool isPuzzleSolved;

    private void Start()
    {
        puzzleSpots = FindObjectsOfType<PuzzleSpot>();

        CreateGridpoints();
    }

    private void CreateGridpoints()
    {
        for (int i=0; i < grid.Length; i++)
        {
            grid[i] = Instantiate(gridpointPrefab, gridpointPosition[i], Quaternion.identity);
        }
    }

    public bool CheckGridpoint(int gridpointNumber)
    {
        bool isOccupied;

        Gridpoint gridpoint = grid[gridpointNumber].GetComponent<Gridpoint>();

        if (gridpoint.isOccupied)
        {
            isOccupied = true;
        }
        else
        {
            isOccupied = false;
        }

        return isOccupied;
    }

    public Vector3 GetGridpointPosition(int gridpointNumber)
    {
        return gridpointPosition[gridpointNumber];
    }

    /*

    private void Update()
    {
        if (!isPuzzleSolved)
        {
            CheckSpotsForBlocks();
        }
    }

    private void CheckSpotsForBlocks()
    {
        bool isBlockOnSpot = false;

        foreach (PuzzleSpot spot in puzzleSpots)
        {
            if (spot.correctBlock)
            {
                isBlockOnSpot = true;
            }
            else
            {
                isBlockOnSpot = false;
                break;
            }
        }
        if (isBlockOnSpot)
        {
            PuzzleSolved();
        }
    }

    private void PuzzleSolved()
    {
        isPuzzleSolved = true;
    }
    */
}
