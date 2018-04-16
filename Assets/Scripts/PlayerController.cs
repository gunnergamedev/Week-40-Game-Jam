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

    private void Start()
    {
        collisionChecker = GetComponent<CollisionChecker>();
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
        velocity.x = directionalInput.x * moveSpeed;
        velocity.y = directionalInput.y * moveSpeed;
        velocity.z = 0f;
    }

    private void MovePlayer()
    {
        collisionChecker.CheckCollisionsAndMove(velocity * Time.deltaTime);
    }

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
}
