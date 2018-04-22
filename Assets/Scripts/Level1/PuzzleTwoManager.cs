using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTwoManager : MonoBehaviour
{
    private MusicManager musicManager;
    private PuzzleButtonTwo puzzleButton;

    private AudioSource audioSource;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip failureSound;
    [SerializeField] private float successSoundDelay;
    [SerializeField] private float musicDelay;

    private bool playedSuccessSound;
    private bool allShellsCorrect;
    private int shellCount;

    private PlayerController player;

    private void Awake()
    {
        musicManager = FindObjectOfType<MusicManager>();
        audioSource = GetComponent<AudioSource>();
        puzzleButton = FindObjectOfType<PuzzleButtonTwo>();
        player = FindObjectOfType<PlayerController>();
        allShellsCorrect = true;
    }

    public void CheckCurrentShell(int shellNumber)
    {
        if (shellNumber != shellCount)
        {
            allShellsCorrect = false;
        }

        shellCount++;
    }

    public void CheckIfAllShellsCorrect()
    {
        if (allShellsCorrect)
        {
            PuzzleSolved();
        }
        else
        {
            PuzzleFailed();
        }
    }

    private void PuzzleSolved()
    {
        ShellPuzzleTwo[] shells = FindObjectsOfType<ShellPuzzleTwo>();

        foreach (ShellPuzzleTwo shell in shells)
        {
            shell.PuzzleSolved();
        }

        musicManager.StopAllMusic();
        puzzleButton.isPuzzleSolved = true;
        StartCoroutine(PuzzleTwoSuccessCo());
    }

    private IEnumerator PuzzleTwoSuccessCo()
    {
        yield return new WaitForSeconds(successSoundDelay);
        PuzzleTwoSuccessSound();
    }

    private void PuzzleTwoSuccessSound()
    {
        if (!playedSuccessSound)
        {
            audioSource.PlayOneShot(successSound);
            playedSuccessSound = true;
        }

        StartCoroutine(PuzzleTwoMusicCo());
    }

    private IEnumerator PuzzleTwoMusicCo()
    {
        yield return new WaitForSeconds(musicDelay);
        PlayMusic();
    }

    private void PlayMusic()
    {
        musicManager.isLevelOneSolved = true;
        musicManager.CheckWhichMusicToPlay();
        player.EnablePlayerMovement();

        this.gameObject.SetActive(false);
    }

    private void PuzzleFailed()
    {
        audioSource.PlayOneShot(failureSound);
        shellCount = 0;
        allShellsCorrect = true;

        ShellPuzzleTwo[] shells = FindObjectsOfType<ShellPuzzleTwo>();

        foreach (ShellPuzzleTwo shell in shells)
        {
            shell.DeactivateShell();
        }

        player.EnablePlayerMovement();
    }

    public void PuzzleAlreadySolved()
    {
        ShellPuzzleTwo[] shells = FindObjectsOfType<ShellPuzzleTwo>();

        foreach (ShellPuzzleTwo shell in shells)
        {
            shell.PuzzleSolved();
            shell.ActivatedButtonSprite();
        }

        puzzleButton.isPuzzleSolved = true;
    }
}
