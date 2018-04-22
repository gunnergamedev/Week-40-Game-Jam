using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject dialogueBox;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeButtonPressed();
        }
    }

    private void EscapeButtonPressed()
    {
        switch (gameIsPaused)
        {
            case true:
                ResumeGame();
                break;

            case false:
                PauseGame();
                break;
        }
    }

    public void ResumeGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1.0f;
        playerController.canMove = true; //for animations, sounds
        HidePauseMenu();
    }

    private void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0.0f;
        playerController.canMove = false; //for animations, sounds
        ShowPauseMenu();
    }

    public void ReturnToMain()
    {
        ResumeGame(); //to reset time scale and stuff
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    private void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }

    public void ActivateDialogueBox()
    {
        dialogueBox.SetActive(true);
    }
}
