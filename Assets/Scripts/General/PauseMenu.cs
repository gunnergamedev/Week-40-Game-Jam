using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;
    [SerializeField] private GameObject pauseMenu;

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
        HidePauseMenu();
    }

    private void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0.0f;
        ShowPauseMenu();
    }

    public void ReturnToMain()
    {
        ResumeGame(); //to reset time scale and stuff
        SceneManager.LoadScene("MainMenu");
    }

    private void ShowPauseMenu()
    {
        pauseMenu.SetActive(true);
    }

    private void HidePauseMenu()
    {
        pauseMenu.SetActive(false);
    }
}
