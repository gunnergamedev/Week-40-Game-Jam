using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButtonTwo : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] puzzleSounds;

    [SerializeField] private float[] delay;

    public bool isPlayingSounds;
    public bool isPuzzleSolved;
    public bool wasButtonActivated;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        yield return new WaitForSeconds(delay[0]);
        PlaySoundOne();
    }

    private void PlaySoundOne()
    {
        audioSource.PlayOneShot(puzzleSounds[0]);
        StartCoroutine(PuzzleSoundTwoCo());
    }

    private IEnumerator PuzzleSoundTwoCo()
    {
        yield return new WaitForSeconds(delay[1]);
        PlaySoundTwo();
    }

    private void PlaySoundTwo()
    {
        audioSource.PlayOneShot(puzzleSounds[1]);
        StartCoroutine(PuzzleSoundThreeCo());
    }

    private IEnumerator PuzzleSoundThreeCo()
    {
        yield return new WaitForSeconds(delay[2]);
        PlaySoundThree();
    }

    private void PlaySoundThree()
    {
        audioSource.PlayOneShot(puzzleSounds[2]);
        StartCoroutine(PuzzleSoundFourCo());
    }

    private IEnumerator PuzzleSoundFourCo()
    {
        yield return new WaitForSeconds(delay[3]);
        PlaySoundFour();
    }

    private void PlaySoundFour()
    {
        audioSource.PlayOneShot(puzzleSounds[3]);
        StartCoroutine(PuzzleSoundFiveCo());
    }

    private IEnumerator PuzzleSoundFiveCo()
    {
        yield return new WaitForSeconds(delay[4]);
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
