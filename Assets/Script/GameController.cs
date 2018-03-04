using System.Collections;
using UnityEngine;
using Tomino;

public class GameController : MonoBehaviour
{
    public Game game = new Game();
    public BoardView boardView;

    void Start()
    {
        game.Start();
        StartCoroutine("Fall");
    }

    void Update()
    {
        var action = GetPlayerAction();
        if (action.HasValue)
        {
            game.HandlePlayerAction(action.Value);
            boardView.RenderGameBoard(game.board);
        }
    }

    PlayerAction? GetPlayerAction()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return PlayerAction.MoveLeft;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return PlayerAction.MoveRight;
        }
        else
        {
            return null;
        }
    }

    IEnumerator Fall()
    {
        while (true)
        {
            game.Update();
            boardView.RenderGameBoard(game.board);

            yield return new WaitForSeconds(1.0f);
        }
    }
}
