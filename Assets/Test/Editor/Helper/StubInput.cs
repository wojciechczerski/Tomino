using Tomino;

public class StubInput : IPlayerInput
{
    public PlayerAction? action;

    public PlayerAction? GetPlayerAction()
    {
        return action;
    }
}
