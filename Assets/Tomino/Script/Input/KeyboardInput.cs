using System.Collections.Generic;
using System.Linq;
using Tomino.Model;
using UnityEngine;

namespace Tomino.Input
{
    public class KeyboardInput : IPlayerInput
    {
        private KeyCode _pressedKey = KeyCode.None;
        private float _nextRepeatedKeyTime;

        private readonly Dictionary<KeyCode, PlayerAction> _actionForKey = new()
        {
            {KeyCode.LeftArrow, PlayerAction.MoveLeft},
            {KeyCode.RightArrow, PlayerAction.MoveRight},
            {KeyCode.DownArrow, PlayerAction.MoveDown},
            {KeyCode.UpArrow, PlayerAction.Rotate},
            {KeyCode.Space, PlayerAction.Fall}
        };

        private readonly List<KeyCode> _repeatingKeys = new()
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
                return _actionForKey[actionKeyDown];
            }

            if (UnityEngine.Input.GetKeyUp(_pressedKey))
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
            _pressedKey = KeyCode.None;
        }

        private void StartKeyRepeatIfPossible(KeyCode key)
        {
            if (!_repeatingKeys.Contains(key)) return;
            _pressedKey = key;
            _nextRepeatedKeyTime = Time.time + Model.Input.KeyRepeatDelay;
        }

        private KeyCode GetActionKeyDown()
        {
            return _actionForKey.Keys.FirstOrDefault(UnityEngine.Input.GetKeyDown);
        }

        private PlayerAction? GetActionForRepeatedKey()
        {
            if (_pressedKey == KeyCode.None || !(Time.time >= _nextRepeatedKeyTime)) return null;

            _nextRepeatedKeyTime = Time.time + Model.Input.KeyRepeatInterval;
            return _actionForKey[_pressedKey];
        }
    }
}
