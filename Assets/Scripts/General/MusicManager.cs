using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioOne;
    [SerializeField] private AudioSource audioTwo;
    [SerializeField] private AudioSource audioThree;
    [SerializeField] private AudioSource audioFull;

    public bool isPuzzleOneSolved;
    public bool isPuzzleTwoSolved;
    public bool isPuzzleThreeSolved;
    public bool areAllPuzzlesSolved;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void CheckWhichMusicToPlay()
    {
        StopAllMusic();

        if (isPuzzleOneSolved && isPuzzleTwoSolved && isPuzzleThreeSolved)
        {
            areAllPuzzlesSolved = true;
            isPuzzleOneSolved = false;
            isPuzzleTwoSolved = false;
            isPuzzleThreeSolved = false;
        }
        if (areAllPuzzlesSolved)
        {
            PlayFullSong();
        }
        if (isPuzzleOneSolved)
        {
            PlayMusicOne();
        }
        if(isPuzzleTwoSolved)
        {
            PlayMusicTwo();
        }
        if(isPuzzleThreeSolved)
        {
            PlayMusicThree();
        }
    }

    private void PlayMusicOne()
    {
        audioOne.Play();
    }

    private void PlayMusicTwo()
    {
        audioTwo.Play();
    }

    private void PlayMusicThree()
    {
        audioThree.Play();
    }

    private void PlayFullSong()
    {
        audioFull.Play();
    }

    private void StopAllMusic()
    {
        audioOne.Stop();
        audioTwo.Stop();
        audioThree.Stop();
        audioFull.Stop();
    }
}
