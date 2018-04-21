using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellPuzzleTwo : MonoBehaviour
{
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private PuzzleTwoManager puzzleManager;

    [SerializeField] private AudioClip[] puzzleSounds;

    [SerializeField] private int shellNumber;

    [SerializeField] private Sprite activatedShell;
    [SerializeField] private Sprite deactivatedShell;

    private bool wasActivated;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        puzzleManager = FindObjectOfType<PuzzleTwoManager>();
    }

    public void ActivateShell()
    {
        if (!wasActivated)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(puzzleSounds[shellNumber]);
            }

            wasActivated = true;
            spriteRenderer.sprite = activatedShell;
            puzzleManager.CheckCurrentShell(shellNumber);
        }
    }

    public void DeactivateShell()
    {
        wasActivated = false;
        spriteRenderer.sprite = deactivatedShell;
    }

    public void PlayShellSound()
    {
        if (!wasActivated)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.PlayOneShot(puzzleSounds[shellNumber]);
        }
    }

    public void PuzzleSolved()
    {
        wasActivated = true;
        spriteRenderer.sprite = activatedShell;
    }
}
