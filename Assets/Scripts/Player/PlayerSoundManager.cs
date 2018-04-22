using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundManager : MonoBehaviour
{
    private GameManager gameManager;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] sandSteps;
    [SerializeField] private AudioClip[] grassSteps;
    [SerializeField] private AudioClip[] caveSteps;

    private bool soundsUnlocked;

    [SerializeField] private float stepDelayMin;
    [SerializeField] private float stepDelayMax;

    public enum Surface { sand, grass, cave };
    public Surface surface;
    public bool isMoving;
    private bool isPlaying;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundsUnlocked = gameManager.soundsUnlocked;

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (soundsUnlocked && isMoving)
        {
            PlayStepsAudio();
        }
    }

    private void Update()
    {
        if (soundsUnlocked && isMoving)
        {
            if (!isPlaying)
            {
                PlayStepsAudio();

                isPlaying = true;
            }
        }
        else
        {
            isPlaying = false;
        }
    }

    public void UnlockSounds()
    {
        soundsUnlocked = true;
    }

    private void PlayStepsAudio()
    {
        CheckWhichSurface();
    }

    private void CheckWhichSurface()
    {
        switch (surface)
        {
            case Surface.cave:
                PlayCaveSteps();
                break;

            case Surface.grass:
                PlayGrassSteps();
                break;

            case Surface.sand:
                PlaySandSteps();
                break;

            default:
                Debug.Log("invalid surface");
                break;
        }
    }

    private void PlayCaveSteps()
    {
        int step = Random.Range(0, caveSteps.Length - 1);

        audioSource.PlayOneShot(caveSteps[step]);

        StartCoroutine(IsPlayingCo());
    }

    private void PlayGrassSteps()
    {
        int step = Random.Range(0, grassSteps.Length - 1);

        audioSource.PlayOneShot(grassSteps[step]);

        StartCoroutine(IsPlayingCo());
    }

    private void PlaySandSteps()
    {
        int step = Random.Range(0, sandSteps.Length - 1);

        audioSource.PlayOneShot(sandSteps[step]);

        StartCoroutine(IsPlayingCo());
    }

    private IEnumerator IsPlayingCo()
    {
        float d = Random.Range(stepDelayMin, stepDelayMax);

        yield return new WaitForSeconds(d);

        isPlaying = false;
    }
}
