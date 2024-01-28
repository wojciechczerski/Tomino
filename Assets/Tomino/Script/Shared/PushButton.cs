using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Tomino.Shared
{
    /// <summary>
    /// A button that sends an event when it pressed or pressed and held.
    /// </summary>
    public class PushButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        /// <summary>
        /// The event triggered after user pushes the button.
        /// </summary>
        public UnityEvent onPush = new();

        /// <summary>
        /// The delay in seconds required to register the 'Push and hold' event. 
        /// </summary>
        public float pushAndHoldDelay = Model.Input.KeyRepeatDelay;

        /// <summary>
        /// The event triggered after the user pushes and holds the button.
        /// </summary>
        public UnityEvent onPushAndHold = new();

        /// <summary>
        /// If set to true, the 'Push and hold' event will repeatedly fire with
        /// specified interval.
        /// </summary>
        public bool repeatPushAndHold = true;

        /// <summary>
        /// The interval in seconds in which 'Push and hold' event repeatedly fires.
        /// </summary>
        public float pushAndHoldRepeatInterval = Model.Input.KeyRepeatInterval;

        public void OnPointerDown(PointerEventData eventData)
        {
            onPush.Invoke();
            _ = StartCoroutine(PushAndHold());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StopAllCoroutines();
        }

        private IEnumerator PushAndHold()
        {
            yield return new WaitForSeconds(pushAndHoldDelay);
            onPushAndHold.Invoke();

            while (repeatPushAndHold)
            {
                yield return new WaitForSeconds(pushAndHoldRepeatInterval);
                onPushAndHold.Invoke();
            }
        }
    }
}
