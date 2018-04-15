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

    private void Start()
    {
        collisionChecker = GetComponent<CollisionChecker>();
    }

    void Update ()
    {
        GetInput();
        MovePlayer();
	}

    private void GetInput()
    {
        directionalInput.x = Input.GetAxis("Horizontal");
        directionalInput.y = Input.GetAxis("Vertical");
    }

    private void MovePlayer()
    {
        velocity.x = directionalInput.x * moveSpeed;
        velocity.y = directionalInput.y * moveSpeed;
        velocity.z = 0f;

        collisionChecker.CheckCollisionsAndMove(velocity * Time.deltaTime);
    }
}
