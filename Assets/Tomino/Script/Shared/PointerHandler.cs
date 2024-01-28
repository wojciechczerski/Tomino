using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Tomino.Shared
{
    public class PointerHandler : MonoBehaviour, IPointerDownHandler
    {
        public UnityEvent onPointerDown = new();

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            onPointerDown.Invoke();
        }
    }
}
