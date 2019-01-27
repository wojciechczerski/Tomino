namespace Tomino
{
    public interface IPlayerInput
    {
        PlayerAction? GetPlayerAction();
        void Update();
        void Cancel();
    }
}
