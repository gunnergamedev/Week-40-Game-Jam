using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Vector3[] mountainExitPositions;
    [SerializeField] private GameObject gridpointToSpawn;

    private PlayerController player;
    public bool playerExitingMountain;

    public int currentMountainNumber;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Sandbox" && playerExitingMountain)
        {
            foreach (Vector3 position in mountainExitPositions)
            {
                Instantiate(gridpointToSpawn, position, Quaternion.identity);
            }

            player = FindObjectOfType<PlayerController>();

            if (player != null)
            {
                player.transform.position = mountainExitPositions[currentMountainNumber];
            }
        }
        else
        {
            playerExitingMountain = false;
        }
    }
}
