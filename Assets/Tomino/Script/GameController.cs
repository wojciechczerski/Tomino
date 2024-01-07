using Constant;
using Tomino;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameConfig gameConfig;
    public AlertView alertView;
    public SettingsView settingsView;
    public AudioPlayer audioPlayer;
    public GameObject screenButtons;
    public AudioSource musicAudioSource;

    private Game _game;
    private UniversalInput _universalInput;

    internal void Awake()
    {
        HandlePlayerSettings();
        Settings.changedEvent += HandlePlayerSettings;
    }

    internal void Start()
    {
        Board board = new(10, 20);

        gameConfig.boardView.SetBoard(board);
        gameConfig.nextPieceView.SetBoard(board);

        _universalInput = new UniversalInput(new KeyboardInput(), gameConfig.boardView.touchInput);

        _game = new Game(board, _universalInput);
        _game.FinishedEvent += OnGameFinished;
        _game.PieceFinishedFallingEvent += audioPlayer.PlayPieceDropClip;
        _game.PieceRotatedEvent += audioPlayer.PlayPieceRotateClip;
        _game.PieceMovedEvent += audioPlayer.PlayPieceMoveClip;
        _game.Start();

        gameConfig.scoreView.game = _game;
        gameConfig.levelView.game = _game;
    }

    public void OnPauseButtonTap()
    {
        _game.Pause();
        ShowPauseView();
    }

    public void OnMoveLeftButtonTap()
    {
        _game.SetNextAction(PlayerAction.MoveLeft);
    }

    public void OnMoveRightButtonTap()
    {
        _game.SetNextAction(PlayerAction.MoveRight);
    }

    public void OnMoveDownButtonTap()
    {
        _game.SetNextAction(PlayerAction.MoveDown);
    }

    public void OnFallButtonTap()
    {
        _game.SetNextAction(PlayerAction.Fall);
    }

    public void OnRotateButtonTap()
    {
        _game.SetNextAction(PlayerAction.Rotate);
    }

    private void OnGameFinished()
    {
        alertView.SetTitle(Text.GameFinished);
        alertView.AddButton(Text.PlayAgain, _game.Start, audioPlayer.PlayNewGameClip);
        alertView.Show();
    }

    internal void Update()
    {
        _game.Update(Time.deltaTime);
    }

    private void ShowPauseView()
    {
        alertView.SetTitle(Text.GamePaused);
        alertView.AddButton(Text.Resume, _game.Resume, audioPlayer.PlayResumeClip);
        alertView.AddButton(Text.NewGame, _game.Start, audioPlayer.PlayNewGameClip);
        alertView.AddButton(Text.Settings, ShowSettingsView, audioPlayer.PlayResumeClip);
        alertView.Show();
    }

    private void ShowSettingsView()
    {
        settingsView.Show(ShowPauseView);
    }

    private void HandlePlayerSettings()
    {
        screenButtons.SetActive(Settings.ScreenButonsEnabled);
        gameConfig.boardView.touchInput.Enabled = !Settings.ScreenButonsEnabled;
        musicAudioSource.gameObject.SetActive(Settings.MusicEnabled);
    }
}
