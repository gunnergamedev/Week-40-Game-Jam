using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pearl : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] puzzleSounds;

    public int pearlNumber;
    public int startingGridPos;
    public int currentGridPos;
    private int targetGridPos;

    private PuzzleOneManager puzzleOneManager;
    [SerializeField] private GameObject interactZone;

    [SerializeField] private float moveSpeed = 3f;

    private PlayerController player;

    private bool isMoving;
    private bool canMove = true;
    private bool isPuzzleSoundPlaying;

    private void Start()
    {
        puzzleOneManager = FindObjectOfType<PuzzleOneManager>();
        currentGridPos = startingGridPos;
        targetGridPos = currentGridPos;
        player = FindObjectOfType<PlayerController>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
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
        else if (other.tag == "Clamshell")
        {
            DeactivatePearl();
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

    private void DeactivatePearl()
    {
        spriteRenderer.enabled = false;
        StartCoroutine(TurnOffPearlMovement());
    }

    private IEnumerator TurnOffPearlMovement()
    {
        yield return new WaitForSeconds(0.3f);
        canMove = false;
    }

    public void MoveUp()
    {
        if (currentGridPos < 10)
        {
            targetGridPos = currentGridPos + 5;
            CheckIfOccupied();
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
        bool isOccupied = puzzleOneManager.CheckGridpoint(targetGridPos);

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
        if (isMoving && canMove)
        {
            Vector3 targetPosition = puzzleOneManager.GetGridpointPosition(targetGridPos);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            player.canMove = false;
            PlayPuzzleSoundWhenPushed();

            if (targetGridPos > 9)
            {
                interactZone.SetActive(false);
            }

            if (transform.position == targetPosition)
            {
                currentGridPos = targetGridPos;
                isMoving = false;
                player.EnablePlayerMovement();
                audioSource.Stop();
                isPuzzleSoundPlaying = false;
            }
        }
    }

    private void PlayPuzzleSoundWhenPushed()
    {
        if (!audioSource.isPlaying && !isPuzzleSoundPlaying)
        {
            audioSource.PlayOneShot(puzzleSounds[pearlNumber]);
            isPuzzleSoundPlaying = true; //to prevent sound from looping, only let it play again when reaches target grid position
        }
    }

    public void PlayPuzzleSound()
    {
        audioSource.PlayOneShot(puzzleSounds[pearlNumber]);
    }
}
