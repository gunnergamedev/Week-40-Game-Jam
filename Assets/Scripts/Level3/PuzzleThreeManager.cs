using UnityEngine;
using System.Collections;

public class PuzzleThreeManager : MonoBehaviour
{
    private MusicManager musicManager;
    private PuzzleButtonThree puzzleButton;
    private AudioSource audioSource;

    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip failureSound;
    [SerializeField] private float successSoundDelay;
    [SerializeField] private float musicDelay;

    private bool playedSuccessSound;

    private Piece[] pieces;
    private int correctPieceCount;

    private PlayerController player;

    private void Awake()
    {
        pieces = FindObjectsOfType<Piece>();
        puzzleButton = FindObjectOfType<PuzzleButtonThree>();
        player = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
        audioSource = GetComponent<AudioSource>();        
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

    private void PuzzleSolved()
    {
        musicManager.StopAllMusic();
        puzzleButton.isPuzzleSolved = true;
        puzzleButton.PlayPuzzleSoundsSolved();
        StartCoroutine(PuzzleThreeSuccessCo());
    }

    private IEnumerator PuzzleThreeSuccessCo()
    {
        yield return new WaitForSeconds(successSoundDelay);
        PuzzleThreeSuccessSound();
    }

    private void PuzzleThreeSuccessSound()
    {
        if (!playedSuccessSound)
        {
            audioSource.PlayOneShot(successSound);
            playedSuccessSound = true;
        }

        StartCoroutine(PuzzleThreeMusicCo());
    }

    private IEnumerator PuzzleThreeMusicCo()
    {
        yield return new WaitForSeconds(musicDelay);
        PlayMusic();
    }

    private void PlayMusic()
    {
        musicManager.isLevelThreeSolved = true;
        musicManager.CheckWhichMusicToPlay();
        player.canMove = true;

        this.gameObject.SetActive(false);
    }

    public void PuzzleAlreadySolved()
    {
        foreach (Piece piece in pieces)
        {
            piece.isPuzzleSolved = true;
        }

        puzzleButton.isPuzzleSolved = true;
    }
}