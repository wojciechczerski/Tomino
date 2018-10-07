using UnityEngine;
using Tomino;

public class KeyboardInput : IPlayerInput
{
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
