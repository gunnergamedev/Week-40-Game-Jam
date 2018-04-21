using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MountainEntry : MonoBehaviour
{
    [SerializeField] private int sceneToLoad;
    public int mountainNumber;

    private MusicManager musicManager;

    private void Awake()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }

    public void EnterMountain()
    {
        SceneManager.LoadScene(sceneToLoad);
        musicManager.EnterMountainLowerVolume();
    }
}
