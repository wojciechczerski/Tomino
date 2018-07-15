using System.Collections;
using UnityEngine;
using Tomino;

public class GameController : MonoBehaviour, IPlayerInput
{
    public Game game;
    public BoardView boardView;

    void Start()
    {
        game.onBoardChanged += boardView.RenderGameBoard;
        game = new Game(this);
        game.Start();
    }

    void Update()
    {
        game.Update(Time.deltaTime);
    }

    public PlayerAction? GetPlayerAction()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return PlayerAction.MoveLeft;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return PlayerAction.MoveRight;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return PlayerAction.MoveDown;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return PlayerAction.Rotate;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            return PlayerAction.Fall;
        }
        return null;
    }
}
