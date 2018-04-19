﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSortingOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private int defaultSortingOrder;
    private int hiddenSortingOrder = -2;
    private int revealSortingOrder = 4;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultSortingOrder = spriteRenderer.sortingOrder;
    }

    public void HidePlayer()
    {
        spriteRenderer.sortingOrder = hiddenSortingOrder;
    }

    public void ResetPlayerSortingOrder()
    {
        spriteRenderer.sortingOrder = defaultSortingOrder;
    }

    public void RevealPlayer()
    {
        spriteRenderer.sortingOrder = revealSortingOrder;
    }
}
