using System.Collections;
using UnityEngine;
using Tomino;

public class GameController : MonoBehaviour
{
    public Game game = new Game();
    public BoardView boardView;

    void Start()
    {
        game.onBoardChanged += boardView.RenderGameBoard;
        game.Start();
    }

    void Update()
    {
        var action = GetPlayerAction();
        if (action.HasValue)
        {
            game.HandlePlayerAction(action.Value);
        }
        else
        {
            game.Update(Time.deltaTime);
        }
    }

    PlayerAction? GetPlayerAction()
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
