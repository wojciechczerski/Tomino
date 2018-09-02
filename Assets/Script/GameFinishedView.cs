using UnityEngine;

public class GameFinishedView : MonoBehaviour
{
    public delegate void ClickEventHandler();
    public event ClickEventHandler PlayAgainEvent = delegate { };

    public void Show() => gameObject.SetActive(true);

    public void Hide() => gameObject.SetActive(false);

    public void OnPlayAgainButtonClick() => PlayAgainEvent();
}
