using Tomino.Model;
using UnityEngine;

namespace Tomino.Input
{
    public class TouchInput : IPlayerInput
    {
        public float blockSize;
        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;
                _cancelCurrentTouch = false;
                _playerAction = null;
            }
        }

        private Vector2 _initialPosition = Vector2.zero;
        private Vector2 _processedOffset = Vector2.zero;
        private PlayerAction? _playerAction;
        private bool _moveDownDetected;
        private float _touchBeginTime;

        private const float TapMaxDuration = 0.25f;
        private const float TapMaxOffset = 30.0f;
        private const float SwipeMaxDuration = 0.3f;

        private bool _cancelCurrentTouch;
        private bool _enabled = true;

        public void Update()
        {
            _playerAction = null;

            if (UnityEngine.Input.touchCount > 0)
            {
                var touch = UnityEngine.Input.GetTouch(0);

                if (_cancelCurrentTouch)
                {
                    _cancelCurrentTouch &= touch.phase != TouchPhase.Ended;
                }
                else if (touch.phase == TouchPhase.Began)
                {
                    TouchBegan(touch);
                }
                else if (touch.phase == TouchPhase.Moved)
                {
                    var offset = touch.position - _initialPosition - _processedOffset;
                    HandleMove(touch, offset);
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    var touchDuration = Time.time - _touchBeginTime;
                    var offset = (touch.position - _initialPosition).magnitude;

                    if (touchDuration < TapMaxDuration && offset < TapMaxOffset)
                    {
                        _playerAction = PlayerAction.Rotate;
                    }
                    else if (_moveDownDetected && touchDuration < SwipeMaxDuration)
                    {
                        _playerAction = PlayerAction.Fall;
                    }
                }
            }
            else
            {
                _cancelCurrentTouch = false;
            }
        }

        public PlayerAction? GetPlayerAction()
        {
            return Enabled ? _playerAction : null;
        }

        public void Cancel()
        {
            _cancelCurrentTouch |= UnityEngine.Input.touchCount > 0;
        }

        private void TouchBegan(Touch touch)
        {
            _initialPosition = touch.position;
            _processedOffset = Vector2.zero;
            _moveDownDetected = false;
            _touchBeginTime = Time.time;
        }

        private void HandleMove(Touch touch, Vector2 offset)
        {
            if (Mathf.Abs(offset.x) >= blockSize)
            {
                HandleHorizontalMove(touch, offset.x);
                _playerAction = ActionForHorizontalMoveOffset(offset.x);
            }

            if (!(offset.y <= -blockSize)) return;

            HandleVerticalMove(touch);
            _playerAction = PlayerAction.MoveDown;
        }

        private void HandleHorizontalMove(Touch touch, float offset)
        {
            _processedOffset.x += Mathf.Sign(offset) * blockSize;
            _processedOffset.y = (touch.position - _initialPosition).y;
        }

        private void HandleVerticalMove(Touch touch)
        {
            _moveDownDetected = true;
            _processedOffset.y -= blockSize;
            _processedOffset.x = (touch.position - _initialPosition).x;
        }

        private static PlayerAction ActionForHorizontalMoveOffset(float offset)
        {
            return offset > 0 ? PlayerAction.MoveRight : PlayerAction.MoveLeft;
        }
    }
}
