using UnityEngine;
using Tomino;

public class GameController : MonoBehaviour
{
    public Camera currentCamera;
    public Board board = new Board(10, 20, new BalancedRandomPieceProvider());
    public Game game;
    public BoardView boardView;
    public PieceView nextPieceView;
    public ScoreView scoreView;
    public LevelView levelView;
    public GameFinishedView gameFinishedView;
    public UniversalInput input = new UniversalInput();
    public TouchInput touchInput = new TouchInput();

    void Start()
    {
        gameFinishedView.Hide();
        gameFinishedView.PlayAgainEvent += OnPlayAgain;

        boardView.SetBoard(board);
        nextPieceView.SetBoard(board);

        input.Register(new KeyboardInput());
        input.Register(touchInput);

        game = new Game(board, input);
        game.FinishedEvent += OnGameFinished;
        game.PieceFinishedFallingEvent += input.Cancel;
        game.Start();

        scoreView.game = game;
        levelView.game = game;
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
        touchInput.blockSize = boardView.BlockSize();
        input.Update();
        game.Update(Time.deltaTime);
    }
}
