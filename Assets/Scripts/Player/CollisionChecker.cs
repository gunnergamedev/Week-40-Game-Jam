using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RaycastController))]
public class CollisionChecker : MonoBehaviour
{
    public CollisionInfo collisions;
    private RaycastController raycastController;
    private float skinWidth;

    private void Start()
    {
        raycastController = GetComponent<RaycastController>();
        skinWidth = raycastController.skinWidth;
    }

    public void CheckCollisionsAndMove(Vector3 moveAmount)
    {
        collisions.Reset();
        moveAmount = CheckForCollisions(moveAmount);
        Move(moveAmount);
    }
 /*
    private Vector2 CheckForCollisions(Vector3 moveAmount)
    {
        if (!Mathf.Approximately(moveAmount.y,0f))
        {
            VerticalCollisions(ref moveAmount);
        }
        else if (!Mathf.Approximately(moveAmount.x, 0f))
        {
            HorizontalCollisions(ref moveAmount);
        }

        return moveAmount;
    } */


    private Vector2 CheckForCollisions(Vector3 moveAmount)
    {
        HorizontalCollisions(ref moveAmount);
        VerticalCollisions(ref moveAmount);

        return moveAmount;
    }

    public void Move(Vector3 moveAmount)
    {
        transform.Translate(moveAmount);
    }

    private void HorizontalCollisions(ref Vector3 moveAmount)
    {
        float directionX = Mathf.Sign(moveAmount.x);
        float rayLength = Mathf.Abs(moveAmount.x) + skinWidth;

        if (Mathf.Abs(moveAmount.x) < skinWidth)
        {
            rayLength = skinWidth * 2;
        }

        for (int i = 0; i < raycastController.horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastController.raycastOrigins.bottomLeft : raycastController.raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (raycastController.horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, raycastController.collisionMask);
            //Debug.DrawRay(rayOrigin, Vector2.right * directionX, Color.red);

            if (hit)
            {
                moveAmount.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;

                if (hit.transform.tag == "Pearl")
                {
                    collisions.left = directionX == -1;
                    collisions.right = directionX == 1;
                }
            }
        }
    }

    private void VerticalCollisions(ref Vector3 moveAmount)
    {
        float directionY = Mathf.Sign(moveAmount.y);
        float rayLength = Mathf.Abs(moveAmount.y) + skinWidth;

        for (int i = 0; i < raycastController.verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastController.raycastOrigins.bottomLeft : raycastController.raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (raycastController.verticalRaySpacing * i + moveAmount.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, raycastController.collisionMask);
            //Debug.DrawRay(rayOrigin, Vector2.up * directionY, Color.red);

            if (hit)
            {
                moveAmount.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;

                if (hit.transform.tag == "Pearl")
                {
                    collisions.below = directionY == -1;
                    collisions.above = directionY == 1;
                }
            }
        }
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
    }
}
