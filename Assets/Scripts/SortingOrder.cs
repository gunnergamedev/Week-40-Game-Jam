using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrder : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private SpriteRenderer spriteRenderer;

    Bounds bounds;
    Bounds playerBounds;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bounds = spriteRenderer.bounds;
    }

    private void Update()
    {
        float posY = bounds.min.y;

        if (posY > player.transform.position.y)
        {
            spriteRenderer.sortingOrder = -1;
        }
        else
        {
            spriteRenderer.sortingOrder = 1;
        }
    }
}
