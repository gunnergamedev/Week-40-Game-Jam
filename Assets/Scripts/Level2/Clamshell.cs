using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamshell : MonoBehaviour
{
    private Animator animator;
    private PuzzleOneManager puzzleManager;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private int puzzleNumber;
    public bool correctPearl;

    [SerializeField] private Sprite pearlShellSprite;
    [SerializeField] private Sprite noPearlSprite;
    [SerializeField] private Sprite closedShell;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        animator.SetBool("hasPearl", true);

        puzzleManager.pearlCount++;

        if (puzzleManager.pearlCount == 5)
        {
            puzzleManager.CheckClamshellsForPearls();
        }
    }

    public void ResetShell()
    {
        spriteRenderer.sprite = noPearlSprite;
        animator.SetBool("hasPearl", false);

        correctPearl = false;
    }

    public void PuzzleAlreadySolved()
    {
        animator.enabled = false;
        spriteRenderer.sprite = closedShell;
    }
}
