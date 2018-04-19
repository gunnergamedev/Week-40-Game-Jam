using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioOne;
    [SerializeField] private AudioSource audioTwo;
    [SerializeField] private AudioSource audioThree;
    [SerializeField] private AudioSource audioFull;

    public bool isLevelOneSolved;
    public bool isLevelTwoSolved;
    public bool isLevelThreeSolved;
    public bool areAllLevelsSolved;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void CheckWhichMusicToPlay()
    {
        StopAllMusic();

        if (isLevelOneSolved && isLevelTwoSolved && isLevelThreeSolved)
        {
            areAllLevelsSolved = true;
            isLevelOneSolved = false;
            isLevelTwoSolved = false;
            isLevelThreeSolved = false;
        }
        if (areAllLevelsSolved)
        {
            PlayFullSong();
        }
        if (isLevelOneSolved)
        {
            PlayMusicOne();
        }
        if(isLevelTwoSolved)
        {
            PlayMusicTwo();
        }
        if(isLevelThreeSolved)
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
