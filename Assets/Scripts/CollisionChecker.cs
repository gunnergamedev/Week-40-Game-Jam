using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RaycastController))]
public class CollisionChecker : MonoBehaviour
{

    private RaycastController raycastController;

    private float skinWidth;

    private void Start()
    {
        raycastController = GetComponent<RaycastController>();
        skinWidth = raycastController.skinWidth;
    }

    public void CheckCollisionsAndMove(Vector2 moveAmount)
    {
        moveAmount = CheckForCollisions(moveAmount);
        transform.Translate(moveAmount);
    }

    private Vector2 CheckForCollisions(Vector2 moveAmount)
    {
        HorizontalCollisions(ref moveAmount);
        VerticalCollisions(ref moveAmount);

        return moveAmount;
    }

    private void HorizontalCollisions(ref Vector2 moveAmount)
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

            if (hit)
            {
                moveAmount.x = (hit.distance - skinWidth) * directionX;
                rayLength = hit.distance;
            }
        }
    }

    private void VerticalCollisions(ref Vector2 moveAmount)
    {
        float directionY = Mathf.Sign(moveAmount.y);
        float rayLength = Mathf.Abs(moveAmount.y) + skinWidth;

        for (int i = 0; i < raycastController.verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastController.raycastOrigins.bottomLeft : raycastController.raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (raycastController.verticalRaySpacing * i + moveAmount.x);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, raycastController.collisionMask);

            if (hit)
            {
                moveAmount.y = (hit.distance - skinWidth) * directionY;
                rayLength = hit.distance;
            }
        }
    }
}
