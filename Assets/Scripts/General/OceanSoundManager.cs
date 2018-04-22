using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanSoundManager : MonoBehaviour
{

    private GameManager gameManager;

    private AudioSource audioSource;

    private bool soundsUnlocked;

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
            audioSource.Play();
        }
    }

    public void UnlockSounds()
    {
        soundsUnlocked = true;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
