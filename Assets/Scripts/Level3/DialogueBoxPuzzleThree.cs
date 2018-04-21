using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBoxPuzzleThree : MonoBehaviour
{
    Animator animator;

    public bool isActivated;

    private void Start()
    {
        animator = GetComponent<Animator>();
        isActivated = true;
        animator.SetBool("isActivated", isActivated);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Activate"))
        {
            DeactivateDialogueBox();
        }
    }

    public void DeactivateDialogueBox()
    {
        isActivated = false;
        animator.SetBool("isActivated", isActivated);

        PuzzleButtonThree puzzleButton = FindObjectOfType<PuzzleButtonThree>();
        puzzleButton.PlayPuzzleSounds();

        StartCoroutine(DeactivateCo());
    }

    private IEnumerator DeactivateCo()
    {
        yield return new WaitForSeconds(0.25f);
        Deactivate();
    }

    private void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
