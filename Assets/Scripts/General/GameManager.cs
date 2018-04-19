using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private MusicManager musicManager;

    [SerializeField] private Vector3[] mountainExitPositions;
    [SerializeField] private GameObject gridpointToSpawn;

    private PlayerController player;
    public bool playerExitingMountain;

    public int currentMountainNumber;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        musicManager = FindObjectOfType<MusicManager>();
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
        if (scene.name != "MainMenu")
        {
            musicManager.playMainMenuMusic = false;
            musicManager.StopMainMenuMusic();
        }

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

        switch(scene.name)
        {
            case "Level1Puzzle2":
                if (musicManager.isLevelOneSolved || musicManager.areAllLevelsSolved)
                {
                    PuzzleOneManager puzzleOneManager = FindObjectOfType<PuzzleOneManager>();
                    PuzzleButtonOne puzzleOneButton = FindObjectOfType<PuzzleButtonOne>();

                    puzzleOneButton.isPuzzleSolved = true;
                    puzzleOneManager.PuzzleAlreadySolved();
                }
                break;

            case "Level2Puzzle1":
                if (musicManager.isLevelTwoSolved || musicManager.areAllLevelsSolved)
                {
                    PuzzleTwoManager puzzleTwoManager = FindObjectOfType<PuzzleTwoManager>();
                    PuzzleButtonTwo puzzleTwoButton = FindObjectOfType<PuzzleButtonTwo>();

                    puzzleTwoButton.isPuzzleSolved = true;
                    puzzleTwoManager.PuzzleAlreadySolved();
                }
                break;

            case "Level3Puzzle3":
                if (musicManager.isLevelThreeSolved || musicManager.areAllLevelsSolved)
                {
                    PuzzleThreeManager puzzleThreeManager = FindObjectOfType<PuzzleThreeManager>();
                    PuzzleButtonThree puzzleThreeButton = FindObjectOfType<PuzzleButtonThree>();

                    puzzleThreeButton.isPuzzleSolved = true;
                    puzzleThreeManager.PuzzleAlreadySolved();
                }
                break;                

            case "MainMenu":

                musicManager.areAllLevelsSolved = false;
                musicManager.isLevelOneSolved = false;
                musicManager.isLevelTwoSolved = false;
                musicManager.isLevelThreeSolved = false;
                musicManager.playMainMenuMusic = true;
                musicManager.CheckWhichMusicToPlay();
                break;
        }
    }
}
