using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioOne;
    [SerializeField] private AudioSource audioTwo;
    [SerializeField] private AudioSource audioThree;
    [SerializeField] private AudioSource audioFull;
    [SerializeField] private AudioSource audioMenu;

    public bool isLevelOneSolved;
    public bool isLevelTwoSolved;
    public bool isLevelThreeSolved;
    public bool areAllLevelsSolved;
    public bool playMainMenuMusic;

    [SerializeField] private float musicDelay;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void CheckWhichMusicToPlay()
    {
        StopAllMusic();
        StartCoroutine(CheckMusicCo());
    }

    private IEnumerator CheckMusicCo()
    {
        yield return new WaitForSeconds(musicDelay);

        if (isLevelOneSolved && isLevelTwoSolved && isLevelThreeSolved)
        {
            areAllLevelsSolved = true;
            isLevelOneSolved = false;
            isLevelTwoSolved = false;
            isLevelThreeSolved = false;
            PlayFullSong();
            SceneManager.LoadScene("WinScreen");
        }
        else
        {
            if (isLevelOneSolved)
            {
                PlayMusicOne();
            }
            if (isLevelTwoSolved)
            {
                PlayMusicTwo();
            }
            if (isLevelThreeSolved)
            {
                PlayMusicThree();
            }
            if (playMainMenuMusic)
            {
                PlayMainMenuMusic();
            }
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
        audioMenu.Stop();
    }

    public void StopMainMenuMusic()
    {
        audioMenu.Stop();
    }

    private void PlayMainMenuMusic()
    {
        audioMenu.Play();
    }
}
