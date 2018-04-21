using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private float fadeTime;

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

    [SerializeField] private float fullVolume;
    [SerializeField] private float loweredVolume;
    [SerializeField] private float musicDelay;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        audioOne.volume = fullVolume;
        audioTwo.volume = fullVolume;
        audioThree.volume = fullVolume;
        audioFull.volume = fullVolume;
        //don't put audioMenu, leave that at full volume
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

            //reset ALL volumes when going to win screen (in case player restarts game again from main menu)
            audioOne.volume = fullVolume;
            audioTwo.volume = fullVolume;
            audioThree.volume = fullVolume;
            audioFull.volume = fullVolume;

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

    public void StopAllMusic()
    {
        StartCoroutine(FadeOutToZeroVolume(audioOne,fadeTime));
        StartCoroutine(FadeOutToZeroVolume(audioTwo, fadeTime));
        StartCoroutine(FadeOutToZeroVolume(audioThree, fadeTime));
        StartCoroutine(FadeOutToZeroVolume(audioFull, fadeTime));
        StartCoroutine(FadeOutToZeroVolume(audioMenu, fadeTime));
    }

    public void StopMainMenuMusic()
    {
        audioMenu.Stop();
    }

    private void PlayMainMenuMusic()
    {
        audioMenu.Play();
    }

    public void EnterMountainLowerVolume()
    {
        StartCoroutine(FadeOutToLowVolume(audioOne, fadeTime));
        StartCoroutine(FadeOutToLowVolume(audioTwo, fadeTime));
        StartCoroutine(FadeOutToLowVolume(audioThree, fadeTime));
        StartCoroutine(FadeOutToLowVolume(audioFull, fadeTime));
    }

    public void ExitMountainIncreaseVolume()
    {
        StartCoroutine(IncreaseVolumeBackToFull(audioOne, fadeTime));
        StartCoroutine(IncreaseVolumeBackToFull(audioTwo, fadeTime));
        StartCoroutine(IncreaseVolumeBackToFull(audioThree, fadeTime));
        StartCoroutine(IncreaseVolumeBackToFull(audioFull, fadeTime));
    }

    public IEnumerator FadeOutToZeroVolume(AudioSource audioSource, float FadeTime)
    {
        float previousVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= previousVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = previousVolume;
    }

    public IEnumerator FadeOutToLowVolume(AudioSource audioSource, float FadeTime)
    {
        while (audioSource.volume > loweredVolume)
        {
            audioSource.volume -= fullVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
    }

    public IEnumerator IncreaseVolumeBackToFull(AudioSource audioSource, float FadeTime)
    {
        while (audioSource.volume < fullVolume)
        {
            audioSource.volume += fullVolume * Time.deltaTime / FadeTime;

            yield return null;
        }
    }
}
