﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private MusicManager musicManager;

    private void Awake()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }

    public void StopMusicWhenRestartingGame()
    {
        musicManager.CheckWhichMusicToPlay();
    }

    private void Update()
    {
        bool start = Input.GetKeyDown(KeyCode.KeypadEnter);
        bool exit = Input.GetKeyDown(KeyCode.Escape);

        if (start)
        {
            StartGame();
        }
        else if (exit)
        {
            ExitGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
}
