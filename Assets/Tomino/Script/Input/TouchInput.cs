using UnityEngine;
using Tomino;

public class TouchInput : IPlayerInput
{
    public float blockSize;
    public bool Enabled
    {
        get => enabled;
        set
        {
            enabled = value;
            cancelCurrentTouch = false;
            playerAction = null;
        }
    }

    private Vector2 initialPosition = Vector2.zero;
    private Vector2 processedOffset = Vector2.zero;
    private PlayerAction? playerAction;
    private bool moveDownDetected;
    private float touchBeginTime;

    private readonly float tapMaxDuration = 0.25f;
    private readonly float tapMaxOffset = 30.0f;
    private readonly float swipeMaxDuration = 0.3f;

    private bool cancelCurrentTouch;
    private bool enabled = true;

    public void Update()
    {
        playerAction = null;

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (cancelCurrentTouch)
            {
                cancelCurrentTouch &= touch.phase != TouchPhase.Ended;
            }
            else if (touch.phase == TouchPhase.Began)
            {
                TouchBegan(touch);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                var offset = touch.position - initialPosition - processedOffset;
                HandleMove(touch, offset);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                var touchDuration = Time.time - touchBeginTime;
                var offset = (touch.position - initialPosition).magnitude;

                if (touchDuration < tapMaxDuration && offset < tapMaxOffset)
                {
                    playerAction = PlayerAction.Rotate;
                }
                else if (moveDownDetected && touchDuration < swipeMaxDuration)
                {
                    playerAction = PlayerAction.Fall;
                }
            }
        }
        else
        {
            cancelCurrentTouch = false;
        }
    }

    public PlayerAction? GetPlayerAction()
    {
        return Enabled ? playerAction : null;
    }

    public void Cancel()
    {
        cancelCurrentTouch |= Input.touchCount > 0;
    }

    private void TouchBegan(Touch touch)
    {
        initialPosition = touch.position;
        processedOffset = Vector2.zero;
        moveDownDetected = false;
        touchBeginTime = Time.time;
    }

    private void HandleMove(Touch touch, Vector2 offset)
    {
        if (Mathf.Abs(offset.x) >= blockSize)
        {
            HandleHorizontalMove(touch, offset.x);
            playerAction = ActionForHorizontalMoveOffset(offset.x);
        }
        if (offset.y <= -blockSize)
        {
            HandleVerticalMove(touch);
            playerAction = PlayerAction.MoveDown;
        }
    }

    private void HandleHorizontalMove(Touch touch, float offset)
    {
        processedOffset.x += Mathf.Sign(offset) * blockSize;
        processedOffset.y = (touch.position - initialPosition).y;
    }

    private void HandleVerticalMove(Touch touch)
    {
        moveDownDetected = true;
        processedOffset.y -= blockSize;
        processedOffset.x = (touch.position - initialPosition).x;
    }

    private PlayerAction ActionForHorizontalMoveOffset(float offset)
    {
        return offset > 0 ? PlayerAction.MoveRight : PlayerAction.MoveLeft;
    }
}
