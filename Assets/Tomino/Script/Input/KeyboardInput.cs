using UnityEngine;
using Tomino;
using System.Collections.Generic;

public class KeyboardInput : IPlayerInput
{
    private KeyCode pressedKey = KeyCode.None;
    private float nextRepeatedKeyTime;

    private readonly Dictionary<KeyCode, PlayerAction> actionForKey = new()
    {
        {KeyCode.LeftArrow, PlayerAction.MoveLeft},
        {KeyCode.RightArrow, PlayerAction.MoveRight},
        {KeyCode.DownArrow, PlayerAction.MoveDown},
        {KeyCode.UpArrow, PlayerAction.Rotate},
        {KeyCode.Space, PlayerAction.Fall},
    };

    private readonly List<KeyCode> repeatingKeys = new()
    {
        KeyCode.LeftArrow,
        KeyCode.RightArrow,
        KeyCode.DownArrow
    };

    public PlayerAction? GetPlayerAction()
    {
        var actionKeyDown = GetActionKeyDown();
        if (actionKeyDown != KeyCode.None)
        {
            StartKeyRepeatIfPossible(actionKeyDown);
            return actionForKey[actionKeyDown];
        }

        if (Input.GetKeyUp(pressedKey))
        {
            Cancel();
        }
        else
        {
            return GetActionForRepeatedKey();
        }

        return null;
    }

    public void Update() { }

    public void Cancel()
    {
        pressedKey = KeyCode.None;
    }

    private void StartKeyRepeatIfPossible(KeyCode key)
    {
        if (repeatingKeys.Contains(key))
        {
            pressedKey = key;
            nextRepeatedKeyTime = Time.time + Constant.Input.KeyRepeatDelay;
        }
    }

    private KeyCode GetActionKeyDown()
    {
        foreach (var key in actionForKey.Keys)
        {
            if (Input.GetKeyDown(key))
            {
                return key;
            }
        }
        return KeyCode.None;
    }

    private PlayerAction? GetActionForRepeatedKey()
    {
        if (pressedKey != KeyCode.None && Time.time >= nextRepeatedKeyTime)
        {
            nextRepeatedKeyTime = Time.time + Constant.Input.KeyRepeatInterval;
            return actionForKey[pressedKey];
        }
        return null;
    }
}
