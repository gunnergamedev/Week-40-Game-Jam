using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButtonThree : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] puzzleSounds;

    [SerializeField] private float delay;
    [SerializeField] private float shufflePiecesDelay;

    public bool isPlayingSounds;
    public bool isPuzzleSolved;
    public bool wasButtonActivated;

    private Piece[] pieces;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        pieces = FindObjectsOfType<Piece>();
    }

    public void ActivateDialogueBox()
    {
        PauseMenu uiManager = FindObjectOfType<PauseMenu>();
        uiManager.ActivateDialogueBox();
    }

    public void PlayPuzzleSounds()
    {
        if (!isPlayingSounds && !isPuzzleSolved)
        {
            wasButtonActivated = true;
            animator.SetBool("wasButtonActivated", wasButtonActivated);
            StartCoroutine(PuzzleSoundOneCo());
            isPlayingSounds = true;
        }
    }

    public void PlayPuzzleSoundsSolved()
    {
        if (!isPlayingSounds)
        {
            StartCoroutine(PuzzleSoundOneCo());
            isPlayingSounds = true;
        }
    }

    private IEnumerator PuzzleSoundOneCo()
    {
        yield return new WaitForSeconds(delay);
        PlaySoundOne();
    }

    private void PlaySoundOne()
    {
        audioSource.PlayOneShot(puzzleSounds[0]);
        StartCoroutine(PuzzleSoundTwoCo());
    }

    private IEnumerator PuzzleSoundTwoCo()
    {
        yield return new WaitForSeconds(delay);
        PlaySoundTwo();
    }

    private void PlaySoundTwo()
    {
        audioSource.PlayOneShot(puzzleSounds[1]);
        StartCoroutine(PuzzleSoundThreeCo());
    }

    private IEnumerator PuzzleSoundThreeCo()
    {
        yield return new WaitForSeconds(delay);
        PlaySoundThree();
    }

    private void PlaySoundThree()
    {
        audioSource.PlayOneShot(puzzleSounds[2]);
        StartCoroutine(PuzzleSoundFourCo());
    }

    private IEnumerator PuzzleSoundFourCo()
    {
        yield return new WaitForSeconds(delay);
        PlaySoundFour();
    }

    private void PlaySoundFour()
    {
        audioSource.PlayOneShot(puzzleSounds[3]);
        StartCoroutine(PuzzleSoundFiveCo());
    }

    private IEnumerator PuzzleSoundFiveCo()
    {
        yield return new WaitForSeconds(delay);
        PlaySoundFive();
    }

    private void PlaySoundFive()
    {
        audioSource.PlayOneShot(puzzleSounds[4]);
        isPlayingSounds = false;

        if (!isPuzzleSolved)
        {
            StartCoroutine(ShufflePiecesCo());
        }
    }

    private IEnumerator ShufflePiecesCo()
    {
        yield return new WaitForSeconds(shufflePiecesDelay);
        ShufflePieces();
    }

    private void ShufflePieces()
    {
        foreach (Piece piece in pieces)
        {
            piece.ShufflePieces();
            piece.isPuzzleSolved = false;
        }
    }
}
