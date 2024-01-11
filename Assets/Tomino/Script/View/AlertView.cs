using Tomino.View;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AlertView : MonoBehaviour
{
    public Text titleText;
    public RectTransform buttonsContainer;
    public GameObject buttonPrefab;

    internal void Awake()
    {
        Hide();
    }

    public void SetTitle(string text)
    {
        titleText.text = text;
    }

    public void AddButton(string text, UnityAction onClickAction, UnityAction pointerDownAction)
    {
        var buttonGameObject = Instantiate(buttonPrefab);
        var alertButton = buttonGameObject.GetComponent<AlertButtonView>();

        alertButton.PointerHandler.onPointerDown.AddListener(pointerDownAction);
        alertButton.Button.onClick.AddListener(onClickAction);
        alertButton.Button.onClick.AddListener(Hide);
        alertButton.Text.text = text;

        alertButton.RectTransform.SetParent(buttonsContainer, false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        for (var i = buttonsContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(buttonsContainer.GetChild(i).gameObject);
        }
        gameObject.SetActive(false);
    }
}
