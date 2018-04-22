using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerBirds : MonoBehaviour
{
    private GameManager gameManager;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] birds;

    private bool soundsUnlocked;

    [SerializeField] private float birdSoundDelayMin;
    [SerializeField] private float birdSoundDelayMax;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundsUnlocked = gameManager.soundsUnlocked;

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (soundsUnlocked)
        {
            StartCoroutine(BirdSoundCo());
        }
    }

    private IEnumerator BirdSoundCo()
    {
        if (soundsUnlocked)
        {
            float d = Random.Range(birdSoundDelayMin, birdSoundDelayMax);

            yield return new WaitForSeconds(d);
            PlayBirdSound();
        }
    }

    private void PlayBirdSound()
    {
        int a = Random.Range(0, birds.Length-1);

        audioSource.PlayOneShot(birds[a]);

        StartCoroutine(BirdSoundCo());
    }

    public void UnlockSounds()
    {
        soundsUnlocked = true;

        StartCoroutine(BirdSoundCo());
    }
}
