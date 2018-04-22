using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButtonOne : MonoBehaviour
{
    private PuzzleOneManager puzzleManager;
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] puzzleSounds;

    [SerializeField] private float delay;

    public bool isPlayingSounds;
    public bool isPuzzleSolved;
    public bool wasButtonActivated;

    [SerializeField] private int[] pearlNumbers;
    private int pearlSoundCount;

    private Clamshell[] shells;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        puzzleManager = FindObjectOfType<PuzzleOneManager>();

        pearlSoundCount = 0;

        shells = FindObjectsOfType<Clamshell>();
    }

    public void AddPearlSoundToPlay(int pearlNumber, int shellNumber)
    {
        pearlNumbers[shellNumber] = pearlNumber;
        pearlSoundCount++;

        if (pearlSoundCount == 5)
        {
            PlayPearlSounds();
        }
    }

    public void PlayPearlSounds()
    {
        if (!isPlayingSounds)
        {
            StartCoroutine(PlayPearlSoundOneCo());
            isPlayingSounds = true;
        }
    }

    private IEnumerator PlayPearlSoundOneCo()
    {
        yield return new WaitForSeconds(delay);
        PlayPearlSoundOne();
    }

    private void PlayPearlSoundOne()
    {
        CheckShell(0);
        int i = pearlNumbers[0];
        audioSource.PlayOneShot(puzzleSounds[i]);
        StartCoroutine(PearlSoundTwoCo());
    }

    private IEnumerator PearlSoundTwoCo()
    {
        yield return new WaitForSeconds(delay);
        PlayPearlSoundTwo();
    }

    private void PlayPearlSoundTwo()
    {
        CheckShell(1);
        int i = pearlNumbers[1];
        audioSource.PlayOneShot(puzzleSounds[i]);
        StartCoroutine(PearlSoundThreeCo());
    }

    private IEnumerator PearlSoundThreeCo()
    {
        yield return new WaitForSeconds(delay);
        PlayPearlSoundThree();
    }

    private void PlayPearlSoundThree()
    {
        CheckShell(2);
        int i = pearlNumbers[2];
        audioSource.PlayOneShot(puzzleSounds[i]);
        StartCoroutine(PearlSoundFourCo());
    }

    private IEnumerator PearlSoundFourCo()
    {
        yield return new WaitForSeconds(delay);
        PlayPearlSoundFour();
    }

    private void PlayPearlSoundFour()
    {
        CheckShell(3);
        int i = pearlNumbers[3];
        audioSource.PlayOneShot(puzzleSounds[i]);
        StartCoroutine(PearlSoundFiveCo());
    }

    private IEnumerator PearlSoundFiveCo()
    {
        yield return new WaitForSeconds(delay);
        PlayPearlSoundFive();
    }

    private void PlayPearlSoundFive()
    {
        CheckShell(4);
        int i = pearlNumbers[4];
        audioSource.PlayOneShot(puzzleSounds[i]);
        isPlayingSounds = false;
        pearlSoundCount = 0;

        StartCoroutine(CheckPuzzleSolved());
    }

    private IEnumerator CheckPuzzleSolved()
    {
        yield return new WaitForSeconds(0.25f);
        puzzleManager.CheckClamshellsForPearls();
    }

    private void CheckShell(int shellNumber)
    {
        foreach(Clamshell shell in shells)
        {
            if (shell.puzzleNumber == shellNumber)
            {
                shell.CheckShellCorrectAnim();
            }
        }
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
            wasButtonActivated = false;
            animator.SetBool("wasButtonActivated", wasButtonActivated);
        }
    }
}
