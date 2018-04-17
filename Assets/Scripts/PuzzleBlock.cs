using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBlock : MonoBehaviour
{
    public int blockNumber;
    public int currentGridPos;
    private int targetGridPos;

    private PuzzleManager puzzleManager;

    [SerializeField] private float moveSpeed = 3f;

    private PlayerController player;

    private bool isMoving;

    private void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
        targetGridPos = currentGridPos;
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Gridpoint")
        {
            Gridpoint gridpoint = other.GetComponent<Gridpoint>();
            gridpoint.isOccupied = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Gridpoint")
        {
            Gridpoint gridpoint = other.GetComponent<Gridpoint>();
            gridpoint.isOccupied = false;
        }
    }

    public void MoveUp()
    {
        if (currentGridPos < 10)
        {
            targetGridPos = currentGridPos + 5;
            CheckIfOccupied();
        }
        else
        {
            switch(currentGridPos)
            {
                case 10:
                    //PuzzleSpot 1
                    break;

                case 11:
                    //2
                    break;

                case 12:
                    //3
                    break;

                case 13:
                    //4
                    break;

                case 14:
                    //5
                    break;

                default:
                    //nothing
                    break;
            }
        }
    }

    public void MoveDown()
    {
        if (currentGridPos > 4)
        {
            targetGridPos = currentGridPos - 5;
            CheckIfOccupied();
        }
    }

    public void MoveLeft()
    {
        if (currentGridPos != 0 && currentGridPos != 5 && currentGridPos != 10)
        {
            targetGridPos = currentGridPos - 1;
            CheckIfOccupied();
        }
    }

    public void MoveRight()
    {
        if (currentGridPos != 4 && currentGridPos != 9 && currentGridPos != 14)
        {
            targetGridPos = currentGridPos + 1;
            CheckIfOccupied();
        }
    }

    private void CheckIfOccupied()
    {
        bool isOccupied = puzzleManager.CheckGridpoint(targetGridPos);

        if (!isOccupied && !isMoving)
        {
            isMoving = true;
        }
        else
        {
            targetGridPos = currentGridPos;
            isMoving = false;
        }
    }

    private void Move()
    {
        if (isMoving)
        {
            Vector3 targetPosition = puzzleManager.GetGridpointPosition(targetGridPos);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            player.canMove = false;

            if (transform.position == targetPosition)
            {
                currentGridPos = targetGridPos;
                isMoving = false;
                player.canMove = true;
            }
        }
    }
}
