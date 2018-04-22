using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButtonTwo : MonoBehaviour
{
    private PuzzleTwoManager puzzleManager;
    private PlayerController player;

    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] puzzleSounds;

    [SerializeField] private float delay;

    public bool isPlayingSounds;
    public bool isPuzzleSolved;
    public bool wasButtonActivated;

    private int soundCount;
    [SerializeField] private int[] soundsToPlay;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        puzzleManager = FindObjectOfType<PuzzleTwoManager>();

        soundCount = 0;
    }

    public void AddShellSoundToPlay(int shellNumber)
    {
        soundsToPlay[soundCount] = shellNumber;
        soundCount++;

        if (soundCount == 5)
        {
            player.canMove = false;
            PlayShellSounds();
        }
    }

    public void PlayShellSounds()
    {
        if (!isPlayingSounds)
        {
            StartCoroutine(PlayShellSoundOneCo());
            isPlayingSounds = true;
        }
    }

    private IEnumerator PlayShellSoundOneCo()
    {
        yield return new WaitForSeconds(delay);
        PlayShellSoundOne();
    }

    private void PlayShellSoundOne()
    {
        int i = soundsToPlay[0];
        audioSource.PlayOneShot(puzzleSounds[i]);
        StartCoroutine(ShellSoundTwoCo());
    }

    private IEnumerator ShellSoundTwoCo()
    {
        yield return new WaitForSeconds(delay);
        PlayShellSoundTwo();
    }

    private void PlayShellSoundTwo()
    {
        int i = soundsToPlay[1];
        audioSource.PlayOneShot(puzzleSounds[i]);
        StartCoroutine(ShellSoundThreeCo());
    }

    private IEnumerator ShellSoundThreeCo()
    {
        yield return new WaitForSeconds(delay);
        PlayShellSoundThree();
    }

    private void PlayShellSoundThree()
    {
        int i = soundsToPlay[2];
        audioSource.PlayOneShot(puzzleSounds[i]);
        StartCoroutine(ShellSoundFourCo());
    }

    private IEnumerator ShellSoundFourCo()
    {
        yield return new WaitForSeconds(delay);
        PlayShellSoundFour();
    }

    private void PlayShellSoundFour()
    {
        int i = soundsToPlay[3];
        audioSource.PlayOneShot(puzzleSounds[i]);
        StartCoroutine(ShellSoundFiveCo());
    }

    private IEnumerator ShellSoundFiveCo()
    {
        yield return new WaitForSeconds(delay);
        PlayShellSoundFive();
    }

    private void PlayShellSoundFive()
    {
        int i = soundsToPlay[4];
        audioSource.PlayOneShot(puzzleSounds[i]);
        isPlayingSounds = false;
        soundCount = 0;

        StartCoroutine(CheckPuzzleSolved());
    }

    private IEnumerator CheckPuzzleSolved()
    {
        yield return new WaitForSeconds(0.25f);
        puzzleManager.CheckIfAllShellsCorrect();
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
            wasButtonActivated = false;
            animator.SetBool("wasButtonActivated", wasButtonActivated);
        }
    }
}
