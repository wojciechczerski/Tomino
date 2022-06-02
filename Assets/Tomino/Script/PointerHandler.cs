using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerHandler : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent onPointerDown = new();

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        onPointerDown.Invoke();
    }
}
