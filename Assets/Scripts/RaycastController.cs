using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class RaycastController : MonoBehaviour
{

    public LayerMask collisionMask;
    private BoxCollider2D boxCollider;

    private const float distBetweenRays = 0.25f;
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
        CalculateRaySpacing();
    }

    private void Update()
    {
        UpdateRaycastOrigins();
    }

    public void UpdateRaycastOrigins()
    {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(skinWidth * -2); //multiplied by -2 to shrink bounds, rather than expand

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    private void CalculateRaySpacing()
    {
        Bounds bounds = boxCollider.bounds;
        bounds.Expand(skinWidth * -2);

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
