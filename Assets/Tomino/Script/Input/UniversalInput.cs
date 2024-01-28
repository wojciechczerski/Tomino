using System.Collections.Generic;
using Tomino.Model;

namespace Tomino.Input
{
    public class UniversalInput : IPlayerInput
    {
        private readonly List<IPlayerInput> _inputs;

        public UniversalInput(params IPlayerInput[] inputs)
        {
            _inputs = new List<IPlayerInput>(inputs);
        }

        public void Update()
        {
            _inputs.ForEach(input => input.Update());
        }

        public void Cancel()
        {
            _inputs.ForEach(input => input.Cancel());
        }

        public PlayerAction? GetPlayerAction()
        {
            foreach (var input in _inputs)
            {
                var action = input.GetPlayerAction();
                if (action != null)
                {
                    return action;
                }
            }
            return null;
        }
    }
}
