using Tomino.Input;
using Tomino.Model;

namespace Tomino.Test.Editor.Helper
{
    public class StubInput : IPlayerInput
    {
        public PlayerAction? action;

        public void Update() { }

        public void Cancel() { }

        public PlayerAction? GetPlayerAction()
        {
            return action;
        }
    }
}
