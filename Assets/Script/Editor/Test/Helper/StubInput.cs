using Tomino;

public class StubInput : IPlayerInput
{
    public PlayerAction? action = null;

    public PlayerAction? GetPlayerAction()
    {
        return action;
    }
}
