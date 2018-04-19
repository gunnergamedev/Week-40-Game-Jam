using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MountainEntry : MonoBehaviour
{
    [SerializeField] private int sceneToLoad;
    public int mountainNumber;

    public void EnterMountain()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
