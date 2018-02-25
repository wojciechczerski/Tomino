using System.Collections;
using UnityEngine;
using Tomino;

public class GameController : MonoBehaviour
{
    public Game game = new Game();
    public BoardView boardView;

    void Start()
    {
        game.SetPiece(new OPiece());

        StartCoroutine("Fall");
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
