using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingOrder : MonoBehaviour
{
    private PlayerController player;
    private SpriteRenderer spriteRenderer;
    private Collider2D coll;

    Bounds bounds;
    Bounds playerBounds;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        bounds = coll.bounds;
        player = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        bounds = coll.bounds;
        playerBounds = player.GetComponent<BoxCollider2D>().bounds;

        float posY = bounds.min.y;
        float playerPosY = playerBounds.min.y;

        if (posY > playerPosY)
        {
            spriteRenderer.sortingOrder = 1;
        }
        else
        {
            spriteRenderer.sortingOrder = 3;
        }
    }
}
