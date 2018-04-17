using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionChecker))]
[RequireComponent(typeof(RaycastController))]
public class PlayerController : MonoBehaviour
{
    private CollisionChecker collisionChecker;
    
    [SerializeField] private float moveSpeed = 5f;
    private Vector3 directionalInput;
    private Vector3 velocity;

    public bool pushingObject;
    public bool canMove;

    private void Start()
    {
        collisionChecker = GetComponent<CollisionChecker>();
        canMove = true;
    }

    void Update ()
    {
        GetInput();
        Move();
	}

    private void GetInput()
    {
        directionalInput.x = Input.GetAxis("Horizontal");
        directionalInput.y = Input.GetAxis("Vertical");
    }

    private void Move()
    {
        SetVelocity();
        MovePlayer();
    }

    private void SetVelocity()
    {
        if (canMove)
        {
            velocity.x = directionalInput.x * moveSpeed;
            velocity.y = directionalInput.y * moveSpeed;
            velocity.z = 0f;
        }
        else
        {
            velocity = Vector3.zero;
        }
    }

    private void MovePlayer()
    {
        collisionChecker.CheckCollisionsAndMove(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pushable")
        {
            PuzzleBlock block = other.GetComponent<PuzzleBlock>();

            if (collisionChecker.collisions.above)
            {
                block.MoveUp();
            }
            else if(collisionChecker.collisions.below)
            {
                block.MoveDown();
            }
            else if(collisionChecker.collisions.left)
            {
                block.MoveLeft();
            }
            else
            {
                block.MoveRight();
            }
        }
    }

    /*
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Pushable")
        {
            if (Input.GetButtonDown("Activate"))
            {
                if (!pushingObject)
                {
                    pushingObject = true;
                    other.transform.parent = transform;
                    other.gameObject.layer = 0;
                }
                else
                {
                    pushingObject = false;
                    other.transform.parent = null;
                    other.gameObject.layer = 8;
                }
            }
        }
    }
    */
}
