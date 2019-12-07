public class GameEventSpy
{
    public bool gameFinishedCalled;

    public void OnGameFinished()
    {
        gameFinishedCalled = true;
    }
}
