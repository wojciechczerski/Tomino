using UnityEngine;
using Tomino;
using System.Collections.Generic;

public class KeyboardInput : IPlayerInput
{
    KeyCode pressedKey = KeyCode.None;
    float nextRepeatedKeyTime;

    Dictionary<KeyCode, PlayerAction> actionForKey = new Dictionary<KeyCode, PlayerAction>
    {
        {KeyCode.LeftArrow, PlayerAction.MoveLeft},
        {KeyCode.RightArrow, PlayerAction.MoveRight},
        {KeyCode.DownArrow, PlayerAction.MoveDown},
        {KeyCode.UpArrow, PlayerAction.Rotate},
        {KeyCode.Space, PlayerAction.Fall},
    };

    readonly List<KeyCode> repeatingKeys = new List<KeyCode>()
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

    void StartKeyRepeatIfPossible(KeyCode key)
    {
        if (repeatingKeys.Contains(key))
        {
            pressedKey = key;
            nextRepeatedKeyTime = Time.time + Constant.Input.KeyRepeatDelay;
        }
    }

    KeyCode GetActionKeyDown()
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

    PlayerAction? GetActionForRepeatedKey()
    {
        if (pressedKey != KeyCode.None && Time.time >= nextRepeatedKeyTime)
        {
            nextRepeatedKeyTime = Time.time + Constant.Input.KeyRepeatInterval;
            return actionForKey[pressedKey];
        }
        return null;
    }
}
