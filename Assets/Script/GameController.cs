using UnityEngine;
using Tomino;

public class GameController : MonoBehaviour
{
    public Camera currentCamera;
    public Board board = new Board(10, 20);
    public Game game;
    public BoardView boardView;
    public ScoreView scoreView;
    public GameFinishedView gameFinishedView;
    public UniversalInput input = new UniversalInput();

    void Start()
    {
        gameFinishedView.Hide();
        gameFinishedView.PlayAgainEvent += OnPlayAgain;

        boardView.gameBoard = board;

        var touchInput = new TouchInput
        {
            blockSize = BlockSizeInPixels()
        };

        input.Register(new KeyboardInput());
        input.Register(touchInput);

        game = new Game(board, input, new RandomPieceProvider());
        game.FinishedEvent += OnGameFinished;
        game.PieceFinishedFallingEvent += input.Cancel;
        game.Start();

        scoreView.game = game;
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
        input.Update();
        game.Update(Time.deltaTime);
    }

    float BlockSizeInPixels()
    {
        var viewportHeight = currentCamera.orthographicSize * 2;
        return currentCamera.pixelHeight / viewportHeight * boardView.BlockSize();
    }
}
