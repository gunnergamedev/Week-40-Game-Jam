using UnityEngine;
using System.Collections;

public class PuzzleThreeManager : MonoBehaviour
{
    private MusicManager musicManager;
    private PuzzleButtonThree puzzleButton;
    private AudioSource audioSource;

    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip failureSound;
    [SerializeField] private float puzzleSolvedMusicDelay;

    private bool playedSuccessSound;

    private Piece[] pieces;
    private int correctPieceCount;

    private void Awake()
    {
        pieces = FindObjectsOfType<Piece>();
    }

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        puzzleButton = FindObjectOfType<PuzzleButtonThree>();
        audioSource = GetComponent<AudioSource>();        
    }

    private void PuzzleSolved()
    {
        if (!playedSuccessSound)
        {
            audioSource.PlayOneShot(successSound);
            playedSuccessSound = true;
        }

        puzzleButton.isPuzzleSolved = true;
        StartCoroutine(PuzzleThreeSolvedCo());
    }

    private IEnumerator PuzzleThreeSolvedCo()
    {
        yield return new WaitForSeconds(puzzleSolvedMusicDelay);
        PuzzleThreeSolved();
    }

    private void PuzzleThreeSolved()
    {
        musicManager.isLevelThreeSolved = true;
        musicManager.CheckWhichMusicToPlay();
        puzzleButton.PlayPuzzleSoundsSolved();
        this.gameObject.SetActive(false);
    }

    public void CheckIfAllPiecesCorrect()
    {
        correctPieceCount = 0;

        foreach (Piece piece in pieces)
        {
            if (piece.values[0] == 1)
            {
                correctPieceCount++;
            }
        }

        if (correctPieceCount == pieces.Length)
        {
            PuzzleSolved();

            foreach (Piece piece in pieces)
            {
                piece.isPuzzleSolved = true;
            }
        }
    }

    public void PuzzleAlreadySolved()
    {
        foreach (Piece piece in pieces)
        {
            piece.isPuzzleSolved = true;
        }
    }
}