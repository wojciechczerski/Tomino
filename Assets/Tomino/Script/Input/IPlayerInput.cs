using Tomino.Model;

namespace Tomino.Input
{
    public interface IPlayerInput
    {
        PlayerAction? GetPlayerAction();
        void Update();
        void Cancel();
    }
}
