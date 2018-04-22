using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellPuzzleTwo : MonoBehaviour
{
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private PuzzleTwoManager puzzleManager;
    private PuzzleButtonTwo puzzleButton;

    [SerializeField] private AudioClip[] puzzleSounds;

    [SerializeField] private int shellNumber;

    [SerializeField] private Sprite activatedShell;
    [SerializeField] private Sprite deactivatedShell;

    [SerializeField] private Sprite buttonActive;
    [SerializeField] private Sprite buttonInactive;
    [SerializeField] private GameObject button;

    SpriteRenderer buttonRenderer;

    private bool wasActivated;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        puzzleManager = FindObjectOfType<PuzzleTwoManager>();
        puzzleButton = FindObjectOfType<PuzzleButtonTwo>();
        buttonRenderer = button.GetComponent<SpriteRenderer>();
    }

    public void ActivatedButtonSprite()
    {
        buttonRenderer.sprite = buttonActive;
    }

    public void InactiveButtonSprite()
    {
        buttonRenderer.sprite = buttonInactive;
    }

    public void ActivateShell()
    {
        if (!wasActivated)
        {
            wasActivated = true;
            spriteRenderer.sprite = activatedShell;
            puzzleManager.CheckCurrentShell(shellNumber);
            puzzleButton.AddShellSoundToPlay(shellNumber);
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
