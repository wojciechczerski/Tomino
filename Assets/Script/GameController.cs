using System.Collections;
using UnityEngine;
using Tomino;

public class GameController : MonoBehaviour, IPlayerInput
{
    public Board board = new Board(10, 20);
    public Game game;
    public BoardView boardView;

    void Start()
    {
        boardView.gameBoard = board;
        game = new Game(board, this, new RandomPieceProvider());
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
