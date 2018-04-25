using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    private void Update()
    {
        bool start = (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Activate") || Input.GetButtonDown("Start"));
        bool exit = Input.GetKeyDown(KeyCode.Escape);

        if (start)
        {
            MainMenu();
        }
        else if (exit)
        {
            ExitGame();
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
