using UnityEngine;
using Tomino;

public class GameController : MonoBehaviour
{
    public Camera currentCamera;
    public Game game;
    public BoardView boardView;
    public PieceView nextPieceView;
    public ScoreView scoreView;
    public LevelView levelView;
    public AlertView alertView;
    public AudioPlayer audioPlayer;

    void Start()
    {
        Board board = new Board(10, 20);

        boardView.SetBoard(board);
        nextPieceView.SetBoard(board);

        game = new Game(board, new UniversalInput(new KeyboardInput(), boardView.touchInput));
        game.FinishedEvent += OnGameFinished;
        game.PieceFinishedFallingEvent += audioPlayer.PlayPieceDropClip;
        game.PieceRotatedEvent += audioPlayer.PlayPieceRotateClip;
        game.PieceMovedEvent += audioPlayer.PlayPieceMoveClip;
        game.Start();

        scoreView.game = game;
        levelView.game = game;
    }

    public void OnPauseButtonTap()
    {
        game.Pause();
        alertView.SetTitle(Constant.Text.GamePaused);
        alertView.AddButton(Constant.Text.Resume, game.Resume, audioPlayer.PlayResumeClip);
        alertView.AddButton(Constant.Text.NewGame, game.Start, audioPlayer.PlayNewGameClip);
        alertView.Show();
    }

    public void OnMoveLeftButtonTap()
    {
        game.SetNextAction(PlayerAction.MoveLeft);
    }

    public void OnMoveRightButtonTap()
    {
        game.SetNextAction(PlayerAction.MoveRight);
    }

    public void OnMoveDownButtonTap()
    {
        game.SetNextAction(PlayerAction.MoveDown);
    }

    public void OnFallButtonTap()
    {
        game.SetNextAction(PlayerAction.Fall);
    }

    public void OnRotateButtonTap()
    {
        game.SetNextAction(PlayerAction.Rotate);
    }

    void OnGameFinished()
    {
        alertView.SetTitle(Constant.Text.GameFinished);
        alertView.AddButton(Constant.Text.PlayAgain, game.Start, audioPlayer.PlayNewGameClip);
        alertView.Show();
    }

    void Update()
    {
        game.Update(Time.deltaTime);
    }
}
