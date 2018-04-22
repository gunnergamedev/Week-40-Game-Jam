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
    private PlayerSoundManager playerSoundManager;
    public bool playerExitingMountain;

    public int currentMountainNumber;

    public bool soundsUnlocked;

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

    public void UnlockSounds()
    {
        soundsUnlocked = true;

        SoundManagerBirds birds = FindObjectOfType<SoundManagerBirds>();
        birds.UnlockSounds();

        playerSoundManager.UnlockSounds();

        OceanSoundManager ocean = FindObjectOfType<OceanSoundManager>();
        ocean.UnlockSounds();
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
                    PuzzleTwoManager puzzleTwoManager = FindObjectOfType<PuzzleTwoManager>();
                    puzzleTwoManager.PuzzleAlreadySolved();
                }

                playerSoundManager = FindObjectOfType<PlayerSoundManager>();
                playerSoundManager.surface = PlayerSoundManager.Surface.cave;

                break;

            case "Level2Puzzle1":
                if (musicManager.isLevelTwoSolved || musicManager.areAllLevelsSolved)
                {
                    PuzzleOneManager puzzleOneManager = FindObjectOfType<PuzzleOneManager>();
                    puzzleOneManager.PuzzleAlreadySolved();
                }

                playerSoundManager = FindObjectOfType<PlayerSoundManager>();
                playerSoundManager.surface = PlayerSoundManager.Surface.cave;
                break;

            case "Level3Puzzle3":
                if (musicManager.isLevelThreeSolved || musicManager.areAllLevelsSolved)
                {
                    PuzzleThreeManager puzzleThreeManager = FindObjectOfType<PuzzleThreeManager>();
                    puzzleThreeManager.PuzzleAlreadySolved();
                }

                playerSoundManager = FindObjectOfType<PlayerSoundManager>();
                playerSoundManager.surface = PlayerSoundManager.Surface.cave;
                break;

            case "Sandbox":
                playerSoundManager = FindObjectOfType<PlayerSoundManager>();
                playerSoundManager.surface = PlayerSoundManager.Surface.sand;

                break;

            case "MainMenu":

                soundsUnlocked = false;

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
