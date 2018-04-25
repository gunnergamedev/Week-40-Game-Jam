using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SignPost : MonoBehaviour
{
    private GameManager gameManager;
    private PauseMenu pauseMenu;

    [SerializeField] private GameObject cursor;

    private bool soundsUnlocked;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundsUnlocked = gameManager.soundsUnlocked;
        pauseMenu = FindObjectOfType<PauseMenu>();

        if (soundsUnlocked == false)
        {
            cursor.SetActive(true);
        }
        else
        {
            cursor.SetActive(false);
        }
    }

    /*
    private void Update()
    {
        if (soundsUnlocked == false)
        {
            CheckMouseClick();
        }
    } */

    public void OnMouseDown()
    {
        if (soundsUnlocked == false && pauseMenu.gameIsPaused == false)
        {
            DialogueBoxRiddle box = FindObjectOfType<DialogueBoxRiddle>();
            box.ActivateDialogueBox();

            PlayerController player = FindObjectOfType<PlayerController>();
            player.canMove = false;

            cursor.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    /*
    private void CheckMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DialogueBoxRiddle box = FindObjectOfType<DialogueBoxRiddle>();
            box.ActivateDialogueBox();

            PlayerController player = FindObjectOfType<PlayerController>();
            player.canMove = false;

            cursor.SetActive(false);
            this.gameObject.SetActive(false);
        }
    } */
}