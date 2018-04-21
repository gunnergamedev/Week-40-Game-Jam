using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionChecker))]
[RequireComponent(typeof(RaycastController))]
public class PlayerController : MonoBehaviour
{
    private CollisionChecker collisionChecker;
    private GameManager gameManager;
    private Animator animator;
    
    [SerializeField] private float moveSpeed = 4f;
    public Vector3 directionalInput;
    private Vector3 velocity;

    private float velocityXSmoothing;
    private float velocityYSmoothing;
    private float accelTime = 0.15f;

    public bool pushingObject;
    public bool canMove;

    private float defaultXScale;

    private void Start()
    {
        collisionChecker = GetComponent<CollisionChecker>();
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        canMove = true;
        defaultXScale = transform.localScale.x;
    }

    void Update ()
    {
        GetInput();
        Move();
        ControlAnimation();
    }

    private void ControlAnimation()
    {
        if (canMove)
        {
            animator.enabled = true;

            bool movingHorz = false;
            bool movingVert = false;
            bool movingUp = false;

            if (directionalInput.x != 0f)
            {
                movingHorz = true;
                Vector3 localScale = transform.localScale;
                localScale.x = defaultXScale * -1 * Mathf.Sign(directionalInput.x);
                transform.localScale = localScale;
            }
            if (directionalInput.y != 0f)
            {
                movingVert = true;
            }
            if (directionalInput.y > 0f)
            {
                movingUp = true;
            }
            else
            {
                movingUp = false;
            }

            animator.SetBool("MovingHorz", movingHorz);
            animator.SetBool("MovingVert", movingVert);
            animator.SetBool("MovingUpwards", movingUp);
        }
        else
        {
            animator.enabled = false;
        }
    }

    private void GetInput()
    {
        directionalInput.x = Input.GetAxisRaw("Horizontal");
        directionalInput.y = Input.GetAxisRaw("Vertical");
    }

    private void Move()
    {
        SetVelocity();
        MovePlayer();
    }
    /*
    private void SetVelocity()
    {
        if (canMove)
        {
            if (!Mathf.Approximately(directionalInput.y, 0f))
            {
                velocity.y = directionalInput.y * moveSpeed;
                velocity.x = 0f;
            }
            else if (!Mathf.Approximately(directionalInput.x, 0f))
            {
                velocity.x = directionalInput.x * moveSpeed;
                velocity.y = 0f;
            }
            else
            {
                velocity.x = 0f;
                velocity.y = 0f;
            }

            velocity.z = 0f;
        }
        else
        {
            velocity = Vector3.zero;
        }
    } */

    private void SetVelocity()
    {
        if (canMove)
        {
            float targetVelocityX = directionalInput.x * moveSpeed;
            float targetVelocityY = directionalInput.y * moveSpeed;
            velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, accelTime);
            velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityY, ref velocityYSmoothing, accelTime);
            velocity.z = 0f;
        }
        else
        {
            velocity = Vector3.zero;
        }
    }

    private void MovePlayer()
    {
        if (canMove)
        {
            collisionChecker.CheckCollisionsAndMove(velocity * Time.deltaTime);
        }
    }

    public void EnablePlayerMovement()
    {
        StartCoroutine(EnablePlayerCo());
    }

    private IEnumerator EnablePlayerCo()
    {
        yield return new WaitForSeconds(0.25f);
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pearl")
        {
            AudioSource pearlAudio = other.GetComponent<AudioSource>();
            if (pearlAudio.isPlaying)
            {
                pearlAudio.Stop();
            }

            Pearl pearl = other.GetComponent<Pearl>();
            
            if (collisionChecker.collisions.above)
            {
                pearl.MoveUp();
            }
            else if(collisionChecker.collisions.below)
            {
                pearl.MoveDown();
            }
            else if(collisionChecker.collisions.left)
            {
                pearl.MoveLeft();
            }
            else if (collisionChecker.collisions.right)
            {
                pearl.MoveRight();
            }
        }

        if (other.tag == "PearlInteractZone")
        {
            Pearl pearl = other.GetComponentInParent<Pearl>();

            if (!pearl.hasBeenPushed)
            {
                pearl.PlayPuzzleSound();
            }
        }

        if (other.tag == "InteractZone")
        {
            InteractZone zone = other.GetComponent<InteractZone>();
            zone.ActivatedButtonSprite();
        }

        if (other.tag == "PuzzleShellTwo")
        {
            ShellPuzzleTwo shell = other.GetComponent<ShellPuzzleTwo>();
            shell.ActivateShell();
        }

        if (other.tag == "PuzzleShellTwoInteractZone")
        {
            ShellPuzzleTwo shell = other.GetComponentInParent<ShellPuzzleTwo>();
            shell.PlayShellSound();
            shell.ActivatedButtonSprite();
        }

        if (other.tag == "PuzzleButtonOne")
        {
            PuzzleButtonOne button = other.GetComponentInParent<PuzzleButtonOne>();

            if (!button.isPuzzleSolved)
            {
                button.PlayPuzzleSounds();
            }
        }

        if (other.tag == "PuzzleButtonTwo")
        {
            PuzzleButtonTwo button = other.GetComponentInParent<PuzzleButtonTwo>();

            if (!button.isPuzzleSolved)
            {
                button.PlayPuzzleSounds();
            }
        }

        if (other.tag == "PuzzleButtonThree")
        {
            PuzzleButtonThree button = other.GetComponent<PuzzleButtonThree>();

            if (!button.isPuzzleSolved)
            {
                button.ActivateDialogueBox();
                canMove = false;
            }
        }

        if (other.tag == "EnterZone")
        {
            MountainEntry mountain = other.GetComponent<MountainEntry>();

            int mountainNumber = mountain.mountainNumber;
            gameManager.currentMountainNumber = mountainNumber;

            mountain.EnterMountain();
        }

        if (other.tag == "ExitZone")
        {
            gameManager.playerExitingMountain = true;
            MountainExit mountain = other.GetComponent<MountainExit>();
            mountain.ExitMountain();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PuzzleShellTwoInteractZone")
        {
            ShellPuzzleTwo shell = other.GetComponentInParent<ShellPuzzleTwo>();
            shell.InactiveButtonSprite();
        }

        if (other.tag == "InteractZone")
        {
            InteractZone zone = other.GetComponent<InteractZone>();

            if (!zone.buttonisActivated)
            {
                zone.InactiveButtonSprite();
            }
        }
    }

    /*private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "PuzzleSound")
        {
            Pearl pearl = other.GetComponentInParent<Pearl>();

            if (pearl != null)
            {
                if (Input.GetButtonDown("Activate"))
                {
                    pearl.PlayPuzzleSound();
                }
            }
        }
    }*/
}
