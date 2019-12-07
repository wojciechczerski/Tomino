using Tomino;
using System.Collections.Generic;

public class UniversalInput : IPlayerInput
{
    List<IPlayerInput> inputs = new List<IPlayerInput>();
    bool isActive = true;

    public void Register(IPlayerInput input)
    {
        inputs.Add(input);
    }

    public void Update()
    {
        if (isActive)
        {
            inputs.ForEach(input => input.Update());
        }
    }

    public void Disable() => isActive = false;

    public void Enable() => isActive = true;

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

