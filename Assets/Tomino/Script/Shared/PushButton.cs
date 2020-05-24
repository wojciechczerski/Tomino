using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

/// <summary>
/// A button that sends an event when it pressed or pressed and held.
/// </summary>
public class PushButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// The event triggered after user pushes the button.
    /// </summary>
    public UnityEvent OnPush = new UnityEvent();

    /// <summary>
    /// The delay in seconds required to register the 'Push and hold' event. 
    /// </summary>
    public float PushAndHoldDelay = Constant.Input.KeyRepeatDelay;

    /// <summary>
    /// The event triggered after the user pushes and holds the button.
    /// </summary>
    public UnityEvent OnPushAndHold = new UnityEvent();

    /// <summary>
    /// If set to true, the 'Push and hold' event will repeatedly fire with
    /// specified interval.
    /// </summary>
    public bool RepeatPushAndHold = true;

    /// <summary>
    /// The interval in seconds in which 'Push and hold' event repeatedly fires.
    /// </summary>
    public float PushAndHoldRepeatInterval = Constant.Input.KeyRepeatInterval;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPush.Invoke();
        StartCoroutine(PushAndHold());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StopAllCoroutines();
    }

    private IEnumerator PushAndHold()
    {
        yield return new WaitForSeconds(PushAndHoldDelay);
        OnPushAndHold.Invoke();

        while (RepeatPushAndHold)
        {
            yield return new WaitForSeconds(PushAndHoldRepeatInterval);
            OnPushAndHold.Invoke();
        }
    }
}
