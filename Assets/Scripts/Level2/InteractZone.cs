using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractZone : MonoBehaviour
{
    [SerializeField] private Sprite buttonActive;
    [SerializeField] private Sprite buttonInactive;

    SpriteRenderer spriteRenderer;

    public bool buttonisActivated;

    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();	
	}

    public void ActivatedButtonSprite()
    {
        spriteRenderer.sprite = buttonActive;
    }

    public void InactiveButtonSprite()
    {
        spriteRenderer.sprite = buttonInactive;
    }
}
