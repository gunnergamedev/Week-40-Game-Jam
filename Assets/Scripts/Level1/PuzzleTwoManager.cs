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
    [SerializeField] private float puzzleSolvedMusicDelay;

    private bool playedSuccessSound;
    private bool allShellsCorrect;
    private int shellCount;

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        audioSource = GetComponent<AudioSource>();
        puzzleButton = FindObjectOfType<PuzzleButtonTwo>();
        allShellsCorrect = true;
    }

    public void CheckCurrentShell(int shellNumber)
    {
        if (shellNumber != shellCount)
        {
            allShellsCorrect = false;
        }

        shellCount++;

        if (shellCount == 5)
        {
            CheckIfAllShellsCorrect();
        }
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
        if (!playedSuccessSound)
        {
            audioSource.PlayOneShot(successSound);
            playedSuccessSound = true;
        }

        ShellPuzzleTwo[] shells = FindObjectsOfType<ShellPuzzleTwo>();

        foreach (ShellPuzzleTwo shell in shells)
        {
            shell.PuzzleSolved();
        }

        puzzleButton.isPuzzleSolved = true;
        StartCoroutine(PuzzleTwoSolvedCo());
    }

    private IEnumerator PuzzleTwoSolvedCo()
    {
        yield return new WaitForSeconds(puzzleSolvedMusicDelay);
        PuzzleTwoSolved();
    }

    private void PuzzleTwoSolved()
    {
        musicManager.isLevelOneSolved = true;
        musicManager.CheckWhichMusicToPlay();
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
    }

    public void PuzzleAlreadySolved()
    {

    }
}
