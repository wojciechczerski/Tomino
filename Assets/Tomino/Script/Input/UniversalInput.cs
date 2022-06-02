using Tomino;
using System.Collections.Generic;

public class UniversalInput : IPlayerInput
{
    private readonly List<IPlayerInput> inputs = new();

    public UniversalInput(params IPlayerInput[] inputs)
    {
        this.inputs = new List<IPlayerInput>(inputs);
    }

    public void Update()
    {
        inputs.ForEach(input => input.Update());
    }

    public void Cancel()
    {
        inputs.ForEach(input => input.Cancel());
    }

    public PlayerAction? GetPlayerAction()
    {
        foreach (var input in inputs)
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
