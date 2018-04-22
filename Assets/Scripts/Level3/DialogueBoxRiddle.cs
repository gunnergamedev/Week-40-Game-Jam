using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBoxRiddle : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] private Animator animator;

    [SerializeField] private GameObject dialogueBox;

    [SerializeField] private AudioClip success;
    [SerializeField] private AudioClip failure;

    private bool closeDialogueBox;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ActivateDialogueBox()
    {
        dialogueBox.SetActive(true);
    }

    public void PlayFailureSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(failure);
        }
    }

    public void Success()
    {
        audioSource.PlayOneShot(success);

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.UnlockSounds();

        PlayerController player = FindObjectOfType<PlayerController>();
        player.canMove = true;

        dialogueBox.SetActive(false);
    }
}
