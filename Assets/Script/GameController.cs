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
        StartCoroutine("Fall");
    }

    void Update()
    {
        var action = GetPlayerAction();
        if (action.HasValue)
        {
            game.HandlePlayerAction(action.Value);
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
        return null;
    }

    IEnumerator Fall()
    {
        while (true)
        {
            game.Update();
            yield return new WaitForSeconds(1.0f);
        }
    }
}
