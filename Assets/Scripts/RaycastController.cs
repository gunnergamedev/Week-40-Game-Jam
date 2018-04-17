using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RaycastController : MonoBehaviour
{
    public LayerMask collisionMask;
    private BoxCollider2D boxCollider;
    private Bounds bounds;

    private const float distBetweenRays = 0.10f;
    [HideInInspector] public float skinWidth = .015f;
    [HideInInspector] public int horizontalRayCount;
    [HideInInspector] public int verticalRayCount;

    [HideInInspector] public float horizontalRaySpacing;
    [HideInInspector] public float verticalRaySpacing;
    [HideInInspector] public RaycastOrigins raycastOrigins;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        bounds = boxCollider.bounds;
        CalculateRaySpacing();
    }

    private void Update()
    {
        UpdateBounds();
        CalculateRaySpacing();
        UpdateRaycastOrigins();
    }

    private void UpdateBounds()
    {
        if (transform.childCount == 0)
        {
            bounds = boxCollider.bounds;
        }
        else
        {
            Bounds newBounds = new Bounds();

            newBounds = boxCollider.bounds;

            Collider2D[] colliders = GetComponentsInChildren<Collider2D>();

            if (colliders.Length > 0)
            {
                foreach (Collider2D coll in colliders)
                {
                    newBounds.Encapsulate(coll.bounds);
                }
            }

            bounds = newBounds;
        }

        bounds.Expand(skinWidth * -2);
    }

    public void UpdateRaycastOrigins()
    {
        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    private void CalculateRaySpacing()
    {
        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;

        horizontalRayCount = Mathf.RoundToInt(boundsHeight / distBetweenRays);
        verticalRayCount = Mathf.RoundToInt(boundsWidth / distBetweenRays);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}
