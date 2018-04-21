using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MountainExit : MonoBehaviour
{
    private MusicManager musicManager;

    private void Awake()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }

    public void ExitMountain()
    {
        musicManager.ExitMountainIncreaseVolume();
        SceneManager.LoadScene(2);
    }
}
