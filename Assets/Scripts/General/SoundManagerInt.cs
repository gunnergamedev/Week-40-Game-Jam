using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerInt : MonoBehaviour
{

    private GameManager gameManager;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] drips;

    private bool soundsUnlocked;

    [SerializeField] private float dripSoundDelayMin;
    [SerializeField] private float dripSoundDelayMax;

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
            PlayDripSound();
        }
    }

    private IEnumerator DripSoundCo()
    {
        if (soundsUnlocked)
        {
            float d = Random.Range(dripSoundDelayMin, dripSoundDelayMax);

            yield return new WaitForSeconds(d);
            PlayDripSound();
        }
    }

    private void PlayDripSound()
    {
        int a = Random.Range(0, drips.Length - 1);

        audioSource.PlayOneShot(drips[a]);

        StartCoroutine(DripSoundCo());
    }

    public void UnlockSounds()
    {
        soundsUnlocked = true;

        StartCoroutine(DripSoundCo());
    }
}
