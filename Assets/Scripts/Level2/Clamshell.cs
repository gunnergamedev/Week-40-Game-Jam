using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamshell : MonoBehaviour
{
    private PuzzleOneManager puzzleManager;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private int puzzleNumber;
    public bool correctPearl;

    [SerializeField] private Sprite pearlShellSprite;
    [SerializeField] private Sprite noPearlSprite;

    private void Start()
    {
        puzzleManager = FindObjectOfType<PuzzleOneManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = noPearlSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Pearl")
        {
            Pearl pearl = other.GetComponent<Pearl>();

            if (puzzleNumber == pearl.pearlNumber)
            {
                correctPearl = true;
            }
            else
            {
                correctPearl = false;
            }

            PearlInShell();
        }
    }

    public void PearlInShell()
    {
        spriteRenderer.sprite = pearlShellSprite;

        puzzleManager.pearlCount++;
    }

    public void ResetShell()
    {
        spriteRenderer.sprite = noPearlSprite;

        correctPearl = false;
    }
}
