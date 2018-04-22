using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clamshell : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public int puzzleNumber;
    public bool correctPearl;

    [SerializeField] private Sprite pearlShellSprite;
    [SerializeField] private Sprite noPearlSprite;
    [SerializeField] private Sprite closedShell;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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

    public void CheckShellCorrectAnim()
    {
        animator.SetBool("correctPearl", correctPearl);
        animator.SetBool("checkingPearls", true);
    }

    public void PearlInShell()
    {
        spriteRenderer.sprite = pearlShellSprite;
        animator.SetBool("hasPearl", true);
    }

    public void ResetShell()
    {
        spriteRenderer.sprite = noPearlSprite;
        animator.SetBool("hasPearl", false);

        correctPearl = false;
        animator.SetBool("correctPearl", false);

        animator.SetBool("resetShell", true);
        animator.SetBool("checkingPearls", false);
    }

    public void ResetAnim()
    {
        animator.SetBool("resetShell", false);
    }

    public void PuzzleAlreadySolved()
    {
        animator.enabled = false;
        spriteRenderer.sprite = closedShell;
    }
}
