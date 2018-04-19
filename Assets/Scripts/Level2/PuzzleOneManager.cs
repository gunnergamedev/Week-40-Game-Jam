using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleOneManager : MonoBehaviour
{
    private MusicManager musicManager;

    private AudioSource audioSource;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip failureSound;
    [SerializeField] private float puzzleSolvedMusicDelay;

    [SerializeField] private GameObject[] grid;
    [SerializeField] private Vector3[] gridpointPosition;
    [SerializeField] private GameObject gridpointPrefab;

    private PlayerController player;
    private PuzzleButtonOne puzzleButton;

    private Clamshell[] clamshells;

    public int pearlCount = 0;

    private bool playedSuccessSound;

    [SerializeField] private float delayAfterFailure;

    [SerializeField] private GameObject pearlToCreate;
    [SerializeField] private GameObject[] pearlObjects;

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        audioSource = GetComponent<AudioSource>();
        clamshells = FindObjectsOfType<Clamshell>();
        CreateGridpoints();
        player = FindObjectOfType<PlayerController>();
        puzzleButton = FindObjectOfType<PuzzleButtonOne>();

        CreatePearls();
    }

    private void CreateGridpoints()
    {
        for (int i=0; i < grid.Length; i++)
        {
            grid[i] = Instantiate(gridpointPrefab, gridpointPosition[i], Quaternion.identity);
        }
    }

    public bool CheckGridpoint(int gridpointNumber)
    {
        bool isOccupied;

        Gridpoint gridpoint = grid[gridpointNumber].GetComponent<Gridpoint>();

        if (gridpoint.isOccupied)
        {
            isOccupied = true;
        }
        else
        {
            isOccupied = false;
        }

        return isOccupied;
    }

    public Vector3 GetGridpointPosition(int gridpointNumber)
    {
        return gridpointPosition[gridpointNumber];
    }

    private void Update()
    {
        CheckClamshellsForPearls();
    }

    private void CheckClamshellsForPearls()
    {
        if (pearlCount == 5)
        {
            CheckIfAllPearlsAreCorrect();
        }
    }

    private void CheckIfAllPearlsAreCorrect()
    {
        int correctPearls = 0;

        foreach (Clamshell shell in clamshells)
        {
            if (shell.correctPearl)
            {
                correctPearls++;
            }
        }
        if (correctPearls == 5)
        {
            PuzzleSolved();
        }
        else
        {
            PuzzleFailed();
        }
    }

    private void PuzzleSolved()
    {
        if (!playedSuccessSound)
        {
            audioSource.PlayOneShot(successSound);
            playedSuccessSound = true;
        }

        StartCoroutine(PuzzleOneSolvedCo());
    }

    private IEnumerator PuzzleOneSolvedCo()
    {
        yield return new WaitForSeconds(puzzleSolvedMusicDelay);
        PuzzleOneSolved();
    }

    private void PuzzleOneSolved()
    {
        musicManager.isLevelTwoSolved = true;
        musicManager.CheckWhichMusicToPlay();
        puzzleButton.isPuzzleSolved = true;        
        this.gameObject.SetActive(false);
    }

    private void PuzzleFailed()
    {
        audioSource.PlayOneShot(failureSound);
        pearlCount = 0;

        DestroyPearls();

        foreach (Clamshell shell in clamshells)
        {
            shell.ResetShell();
        }

        StartCoroutine(CreatePearlsCo());
    }

    private void DestroyPearls()
    {
        foreach (GameObject pearl in pearlObjects)
        {
            Destroy(pearl);
        }
    }

    private IEnumerator CreatePearlsCo()
    {
        yield return new WaitForSeconds(delayAfterFailure);
        player.EnablePlayerMovement();
        CreatePearls();
    }

    private void CreatePearls()
    {
        int pearlCount = 5;

        for (int i = 0; i < pearlCount; i++)
        {
            pearlObjects[i] = Instantiate(pearlToCreate, gridpointPosition[i], Quaternion.identity);

            Pearl pearl = pearlObjects[i].GetComponent<Pearl>();

            pearl.startingGridPos = i;
            
            switch(i)
            {
                case 0:
                    pearl.pearlNumber = 1;
                    break;

                case 1:
                    pearl.pearlNumber = 4;
                    break;

                case 2:
                    pearl.pearlNumber = 0;
                    break;

                case 3:
                    pearl.pearlNumber = 3;
                    break;

                default:
                    pearl.pearlNumber = 2;
                    break;
            }
        }
    }

    public void PuzzleAlreadySolved()
    {

    }
}
