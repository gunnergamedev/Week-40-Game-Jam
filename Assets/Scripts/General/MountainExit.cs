using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MountainExit : MonoBehaviour
{
    public void ExitMountain()
    {
        SceneManager.LoadScene(2);
    }
}
