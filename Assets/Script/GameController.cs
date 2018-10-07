using UnityEngine;
using Tomino;

public class GameController : MonoBehaviour
{
    public Camera currentCamera;
    public Board board = new Board(10, 20);
    public Game game;
    public BoardView boardView;
    public GameFinishedView gameFinishedView;
    public TouchInput touchInput = new TouchInput();

    void Start()
    {
        gameFinishedView.Hide();
        gameFinishedView.PlayAgainEvent += OnPlayAgain;

        boardView.gameBoard = board;
        touchInput.blockSize = BlockSizeInPixels();

        game = new Game(board, touchInput, new RandomPieceProvider());
        game.FinishedEvent += OnGameFinished;
        game.PieceFinishedFallingEvent += touchInput.CancelCurrentTouch;
        game.Start();
    }

    void OnGameFinished()
    {
        gameFinishedView.Show();
    }

    void OnPlayAgain()
    {
        game.Start();
        gameFinishedView.Hide();
    }

    void Update()
    {
        touchInput.Update();
        game.Update(Time.deltaTime);
    }

    float BlockSizeInPixels()
    {
        var viewportHeight = currentCamera.orthographicSize * 2;
        return currentCamera.pixelHeight / viewportHeight * boardView.BlockSize();
    }
}
